using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Models;

namespace Todo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;
    private List<Models.Todo> _todoList;

    public TodoController(AppDbContext appDbContext)
    {
        _todoList = new List<Models.Todo>
        {
            new() { Id = 1, TaskName = "Clean the litter box" },
            new() { Id = 2, TaskName = "Brush teeth" }
        };

        _context = appDbContext;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Models.Todo>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var items = await _context.TodoItems.ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Models.Todo), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(TodoCreateRequest request)
    {
        var todo = new Models.Todo
        {
            TaskName = request.TaskName,
            IsCompleted = false
        };

        _context.TodoItems.Add(todo);

        await _context.SaveChangesAsync();

        return Ok(todo);
    }
}