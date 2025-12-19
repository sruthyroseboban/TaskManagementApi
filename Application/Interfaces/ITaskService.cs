using TaskApi.Domain;

namespace TaskApi.Application.Interfaces;

public interface ITaskService
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(Guid id);
    TaskItem Create(TaskItem task);
    bool MarkComplete(Guid id);
    bool Delete(Guid id);
}
