using TaskApi.Domain;

namespace TaskApi.Application.Interfaces;

public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(Guid id);
    void Add(TaskItem task);
    void Update(TaskItem task);
    void Delete(Guid id);
}
