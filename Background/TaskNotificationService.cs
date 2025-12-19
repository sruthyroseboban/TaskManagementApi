using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskApi.Application.Interfaces;

namespace TaskApi.Background;

public class TaskNotificationService : BackgroundService
{
    private readonly ILogger<TaskNotificationService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public TaskNotificationService(ILogger<TaskNotificationService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();

                var dueSoon = taskService.GetAll()
                    .Where(t => !t.IsCompleted && t.DueAt.HasValue && t.DueAt.Value <= DateTime.UtcNow.AddMinutes(1))
                    .ToList();

                if (dueSoon.Any())
                    _logger.LogInformation("🔔 {Count} tasks due soon.", dueSoon.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Background service error");
            }

            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
