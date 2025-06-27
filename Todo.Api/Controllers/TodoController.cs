using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Models;

namespace Todo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Models.Todo>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var items = await _context.TodoItems.ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Models.Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);

        if (item == null) return NotFound();

        return Ok(item);
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatusAsync(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);

        if (item == null) return NotFound();

        item.IsCompleted = !item.IsCompleted;
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);

        if (item == null) return NotFound();

        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}