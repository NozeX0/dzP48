using Microsoft.AspNetCore.Mvc;
using dz_2.Models;

namespace dz_2.Controllers;

[Route("api")]
[ApiController]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> _tasks = new();

    [HttpGet("tasks")]
    public IActionResult GetTasks([FromQuery] bool? completed, [FromQuery] bool? expired, [FromQuery] bool? current)
    {
        var query = _tasks.AsQueryable();
        if (completed == true) query = query.Where(t => t.Completed);
        if (expired == true) query = query.Where(t => t.Deadline_date < DateTime.UtcNow && !t.Completed);
        if (current == true) query = query.Where(t => t.Deadline_date >= DateTime.UtcNow && !t.Completed);
        return Ok(new ApiResponse<IEnumerable<TaskItem>> { Status = 200, Message = "Ok", Data = query.ToList() });
    }

    [HttpGet("task/{id}")]
    public IActionResult GetTask(Guid id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound(new ApiResponse<object> { Status = 404, Message = "Task not found", Error = "Not Found" });
        return Ok(new ApiResponse<TaskItem> { Status = 200, Message = "Ok", Data = task });
    }

    [HttpPost("tasks/create")]
    public IActionResult CreateTask([FromBody] TaskItem task, [FromHeader(Name = "Authorization")] string? token)
    {
        if (string.IsNullOrEmpty(token)) return StatusCode(403, new ApiResponse<object> { Status = 403, Message = "Forbidden", Error = "Token missing" });
        if (task == null || string.IsNullOrEmpty(task.Title)) return BadRequest(new ApiResponse<object> { Status = 400, Message = "Invalid data", Error = "Title is required" });
        task.Id = Guid.NewGuid();
        _tasks.Add(task);
        return Ok(new ApiResponse<TaskItem> { Status = 200, Message = "Created", Data = task });
    }

    [HttpDelete("task/{id}")]
    public IActionResult DeleteTask(Guid id, [FromHeader(Name = "Authorization")] string? token)
    {
        if (string.IsNullOrEmpty(token)) return StatusCode(403, new ApiResponse<object> { Status = 403, Message = "Forbidden", Error = "Token missing" });
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task == null) return NotFound(new ApiResponse<object> { Status = 404, Message = "Task not found", Error = "Not Found" });
        _tasks.Remove(task);
        return Ok(new ApiResponse<object> { Status = 200, Message = "Deleted" });
    }
}