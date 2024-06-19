// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Validation.FluentValidation.EndpointFilters;

public class AutoValidationEndpointFilterProvider : EndpointFilterProviderBase
{
    private static readonly object Lock = new();
    private static List<Type>? _validatorEntityTypes;
    private readonly IOptions<JsonOptions> _jsonOptions;
    private static readonly ConcurrentDictionary<Type, Type> ValidatorTypes = new();

    public AutoValidationEndpointFilterProvider(IOptions<JsonOptions> jsonOptions)
    {
        if (_validatorEntityTypes is null)
        {
            InitValidatorTypes();
        }
        _jsonOptions = jsonOptions;
    }

    private static void InitValidatorTypes()
    {
        if (_validatorEntityTypes is not null) return;

        lock (Lock)
        {
            if (_validatorEntityTypes is not null) return;

            var validatorType = typeof(IValidator);
            _validatorEntityTypes = App.Instance.Services.Where(service => service.ServiceType.IsAssignableTo(validatorType)).SelectMany(service => service.ServiceType.GenericTypeArguments).ToList();
        }
    }

    public override async ValueTask<object?> HandlerAsync(EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        for (var i = 0; i < invocationContext.Arguments.Count; i++)
        {
            var argument = invocationContext.Arguments[i];

            var type = argument?.GetType();
            if (type is null || !_validatorEntityTypes!.Contains(type)) continue;
            var validatorType = ValidatorTypes.GetOrAdd(type, t => typeof(IValidator<>).MakeGenericType(type));

            if (invocationContext.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
                continue;

            var validationResult = await validator.ValidateAsync(new ValidationContext<object?>(argument), invocationContext.HttpContext.RequestAborted);

            if (validationResult.IsValid)
                continue;

            var data = validationResult.ToDictionary();

            if (_jsonOptions.Value.SerializerOptions.PropertyNamingPolicy != null)
            {
                data = data.ToDictionary(x => _jsonOptions.Value.SerializerOptions.PropertyNamingPolicy.ConvertName(x.Key), x => x.Value);
            }

            return Results.ValidationProblem(data);
        }

        return await next(invocationContext);
    }
}
