namespace TaskApi.Application.Interfaces;

public interface IRequestTracker { Guid RequestId { get; } }

public class RequestTracker : IRequestTracker
{
    public Guid RequestId { get; } = Guid.NewGuid();
}
