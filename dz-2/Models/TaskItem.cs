namespace dz_2.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Completed { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Created_at { get; set; } = DateTime.UtcNow;
    public DateTime Deadline_date { get; set; }
}

public class ApiResponse<T>
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public object? Error { get; set; }
}