namespace Todo.Api.Models;

public class Todo
{
    public int Id { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}