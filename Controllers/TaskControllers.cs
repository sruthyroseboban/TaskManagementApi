using Microsoft.AspNetCore.Mvc;
using TaskApi.Domain;
using TaskApi.Application.Interfaces;

namespace TaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_service.GetAll());

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var task = _service.GetById(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public IActionResult Create([FromBody] TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            return BadRequest("Title is required.");

        var created = _service.Create(task);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}/complete")]
    public IActionResult MarkComplete(Guid id)
        => _service.MarkComplete(id) ? Ok() : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
        => _service.Delete(id) ? NoContent() : NotFound();
}
