using Microsoft.OpenApi.Models;
using TaskApi.Application;
using TaskApi.Application.Interfaces;
using TaskApi.Background;
using TaskApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "Machine Test: Repository pattern + DI lifetimes + BackgroundService + External API integration"
    });
});

builder.Services.AddScoped<ITaskRepository, InMemoryTaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddSingleton<ISystemClock, SystemClock>();

builder.Services.AddTransient<IRequestTracker, RequestTracker>();

builder.Services.AddHostedService<TaskNotificationService>();

builder.Services.AddHttpClient<IExternalApiClient, JsonPlaceholderClient>(c =>
{
    c.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
    c.Timeout = TimeSpan.FromSeconds(5);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
