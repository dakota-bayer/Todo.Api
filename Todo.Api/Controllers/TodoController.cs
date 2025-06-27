using Microsoft.AspNetCore.Mvc;
using Todo.Api.Models;

namespace Todo.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private List<Models.Todo> _todoList;

    public TodoController()
    {
        _todoList = new List<Models.Todo>
        {
            new Models.Todo { Id = 1, TaskName = "Clean the litter box" },
            new Models.Todo { Id = 2, TaskName = "Brush teeth"}
        };
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Models.Todo>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAllAsync()
    {
        return Task.FromResult<IActionResult>(Ok(_todoList));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Models.Todo), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(TodoCreateRequest request)
    {
        var todo = new Models.Todo
        {
            Id = _todoList.Max(i => i.Id) + 1, 
            TaskName = request.TaskName
        };
        
        _todoList.Add(todo);

        return Task.FromResult<IActionResult>(Ok(todo));
    }
}