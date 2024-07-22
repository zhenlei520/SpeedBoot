namespace SpeedBoot.Data.FreeSql.Tests.Components.Interceptors;

public class DbContextInterceptor : IDbContextInterceptor
{
    public int Order => 99;

    public void SaveSucceed(SaveSucceedDbContextEventData eventData)
    {
        Console.WriteLine("SaveSucceed");
    }

    public  Task SaveSucceedAsync(SaveSucceedDbContextEventData eventData, CancellationToken cancellationToken)
    {
        Console.WriteLine("SaveSucceedAsync");
        return Task.CompletedTask;
    }

    public void SaveFailed(SaveFailedDbContextEventData eventData)
    {
    }

    public Task SaveFailedAsync(SaveFailedDbContextEventData eventData, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
