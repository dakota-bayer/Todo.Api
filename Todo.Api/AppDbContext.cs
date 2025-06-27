using Microsoft.EntityFrameworkCore;

namespace Todo.Api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Models.Todo> TodoItems { get; set; }
}