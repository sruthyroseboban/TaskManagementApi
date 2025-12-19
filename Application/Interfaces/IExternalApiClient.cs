namespace TaskApi.Application.Interfaces;

public interface IExternalApiClient
{
    Task<string> GetSampleDataAsync(CancellationToken ct);
}
