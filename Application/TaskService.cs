using TaskApi.Domain;
using TaskApi.Application.Interfaces;

namespace TaskApi.Application;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo) => _repo = repo;

    public IEnumerable<TaskItem> GetAll() => _repo.GetAll();
    public TaskItem? GetById(Guid id) => _repo.GetById(id);

    public TaskItem Create(TaskItem task)
    {
        _repo.Add(task);
        return task;
    }

    public bool MarkComplete(Guid id)
    {
        var task = _repo.GetById(id);
        if (task == null) return false;

        task.IsCompleted = true;
        _repo.Update(task);
        return true;
    }

    public bool Delete(Guid id)
    {
        var task = _repo.GetById(id);
        if (task == null) return false;

        _repo.Delete(id);
        return true;
    }
}
