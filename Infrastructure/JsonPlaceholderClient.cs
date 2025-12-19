using TaskApi.Application.Interfaces;

namespace TaskApi.Infrastructure;

public class JsonPlaceholderClient : IExternalApiClient
{
    private readonly HttpClient _http;
    public JsonPlaceholderClient(HttpClient http) => _http = http;

    public async Task<string> GetSampleDataAsync(CancellationToken ct)
    {
        try
        {
            using var res = await _http.GetAsync("/todos/1", ct);
            if (!res.IsSuccessStatusCode)
                return $"External API failed: {(int)res.StatusCode} {res.ReasonPhrase}";

            return await res.Content.ReadAsStringAsync(ct);
        }
        catch (TaskCanceledException)
        {
            return "External API request timed out.";
        }
        catch (Exception ex)
        {
            return $"External API error: {ex.Message}";
        }
    }
}
