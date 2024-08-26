using efconnectortests.postgresql.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace efconnectortests.postgresql;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> Logger;
    private readonly IServiceProvider ServiceProvider;

    public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
    {
        ServiceProvider = serviceProvider;
        Logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var cnt = 1;
        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = ServiceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TestContext>();
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            context.Database.SetCommandTimeout(3);
            var token = cts.Token;
            using var tx = context.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
            try
            {
                var statement = $"SELECT * FROM test ORDER BY id DESC LIMIT 1 FOR UPDATE";
                var test = await context
                    .Tests
                    .FromSqlRaw(statement)
                    .FirstOrDefaultAsync(token);
                if (test == null)
                {
                    Logger.LogInformation($"#{cnt}: No records found in the database.");
                }
                else
                {
                    Logger.LogInformation($"#{cnt}: Record found in the database: {test.Id}");
                }
                await tx.CommitAsync(token);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An error occurred while querying the database.");
                await tx.RollbackAsync(CancellationToken.None);
            }
            await Task.Delay(2000, stoppingToken);
            cnt++;
        }

    }
}
