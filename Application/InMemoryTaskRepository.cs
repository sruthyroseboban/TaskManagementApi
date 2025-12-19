using TaskApi.Domain;
using TaskApi.Application.Interfaces;

namespace TaskApi.Infrastructure;
public class InMemoryTaskRepository : ITaskRepository
{
    private static readonly List<TaskItem> _tasks = new();

    public IEnumerable<TaskItem> GetAll() => _tasks;

    public TaskItem? GetById(Guid id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Add(TaskItem task) => _tasks.Add(task);

    public void Update(TaskItem task)
    {
        var existing = GetById(task.Id);
        if (existing == null) return;

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.IsCompleted = task.IsCompleted;
        existing.DueAt = task.DueAt;
    }

    public void Delete(Guid id)
    {
        var existing = GetById(id);
        if (existing != null) _tasks.Remove(existing);
    }
}
