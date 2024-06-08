// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.Data.FreeSql.Tests")]

namespace SpeedBoot.Data.FreeSql.Internal.Interceptors;

internal class FreeSqlAuditInterceptor : IDisposable
#if NET5_0_OR_GREATER || NETSTANDARD2_1
    , IAsyncDisposable
#endif
{
    private static CustomConcurrentDictionary<Type, Func<object, Dictionary<string, object>>> _data =
        new CustomConcurrentDictionary<Type, Func<object, Dictionary<string, object>>>();

    private readonly FreeSqlSaveChangesInterceptor _freeSqlSaveChangesInterceptor;
    private readonly IEnumerable<ISaveChangesInterceptor> _saveChangesInterceptors;
    private readonly IEnumerable<IDbContextInterceptor> _dbContextInterceptors;
    private bool _isDispose;

    public FreeSqlAuditInterceptor(
        FreeSqlSaveChangesInterceptor freeSqlSaveChangesInterceptor,
        IEnumerable<ISaveChangesInterceptor> saveChangesInterceptors,
        IEnumerable<IDbContextInterceptor> dbContextInterceptors)
    {
        _freeSqlSaveChangesInterceptor = freeSqlSaveChangesInterceptor;
        _saveChangesInterceptors = saveChangesInterceptors;
        _dbContextInterceptors = dbContextInterceptors;
    }

    public void SetEntity(IServiceProvider serviceProvider, DbContext.EntityChangeReport.ChangeInfo changeInfo)
    {
        var func = _data.GetOrAdd(changeInfo.EntityType, CreatePropertyValuesFunc);

        if (_freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData == null)
            return;

        var oldData = changeInfo.BeforeObject != null ? func.Invoke(changeInfo.BeforeObject) : new Dictionary<string, object>();
        var currentData = changeInfo.Object != null ? func.Invoke(changeInfo.Object) : new Dictionary<string, object>();

        var primaryKeys = SaveChangesUtils.PrimaryKeys.TryGet(changeInfo.EntityType, out var keys) ? keys : Array.Empty<string>();
        var entityInfo = new EntityInfo()
        {
            EntityState = changeInfo.Type.GetEntityState(),
            EntityType = changeInfo.EntityType,
            PropertyInfos = changeInfo.EntityType.GetProperties().Select(p => new EntityPropertyInfo()
            {
                IsPrimaryKey = primaryKeys.Contains(p.Name),
                PropertyName = p.Name,
                PropertyType = p.PropertyType,
                OldValue = oldData.GetValueOrDefault(p.Name),
                NewValue = currentData.GetValueOrDefault(p.Name),
            }).ToList()
        };
        _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.Entites.Add(entityInfo);
    }

    private static Func<object, Dictionary<string, object>> CreatePropertyValuesFunc(Type entityType)
    {
        var parameter = Expression.Parameter(typeof(object), "x");
        var castParameter = Expression.Convert(parameter, entityType);
        var propertyExpressions = entityType.GetProperties();

        var dictionaryType = typeof(Dictionary<string, object>);
        var dictionaryCtor = dictionaryType.GetConstructor(Type.EmptyTypes);
        var addMethod = dictionaryType.GetMethod("Add");

        var dictionaryVar = Expression.Variable(dictionaryType);
        var bodyExpressions = new List<Expression>
        {
            Expression.Assign(dictionaryVar, Expression.New(dictionaryCtor))
        };

        foreach (var prop in propertyExpressions)
        {
            var propValueExpr = Expression.Property(castParameter, prop);
            var convertExpr = Expression.Convert(propValueExpr, typeof(object));
            var addExpr = Expression.Call(dictionaryVar, addMethod, Expression.Constant(prop.Name), convertExpr);
            bodyExpressions.Add(addExpr);
        }

        bodyExpressions.Add(dictionaryVar);

        var blockExpr = Expression.Block(new[] { dictionaryVar }, bodyExpressions);
        var lambdaExpr = Expression.Lambda<Func<object, Dictionary<string, object>>>(blockExpr, parameter);
        return lambdaExpr.Compile();
    }

    public void Dispose()
    {
        if (_isDispose || _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData == null)
            return;

        _isDispose = true;
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            saveChangesInterceptor.SavedChanges(_freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData);
        }

        foreach (var dbContextInterceptor in _dbContextInterceptors)
        {
            dbContextInterceptor.SaveSucceed(new SaveSucceedDbContextEventData()
            {
                EventId = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.EventId,
                EventName = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.EventName,
                ContextId = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.ContextId,
                Entites = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.Entites
            });
        }
    }

#if NET5_0_OR_GREATER || NETSTANDARD2_1
    public async ValueTask DisposeAsync()
    {
        if (_isDispose || _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData == null)
            return;

        _isDispose = true;
        foreach (var saveChangesInterceptor in _saveChangesInterceptors)
        {
            await saveChangesInterceptor.SavedChangesAsync(_freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData);
        }

        foreach (var dbContextInterceptor in _dbContextInterceptors)
        {
            await dbContextInterceptor.SaveSucceedAsync(new SaveSucceedDbContextEventData()
            {
                EventId = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.EventId,
                EventName = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.EventName,
                ContextId = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.ContextId,
                Entites = _freeSqlSaveChangesInterceptor.SaveChangesCompletedEventData.Entites
            }, CancellationToken.None);
        }
    }
#endif
}
