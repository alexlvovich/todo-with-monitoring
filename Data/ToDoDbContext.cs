using Microsoft.EntityFrameworkCore;
using todo.Abstractions;

namespace todo.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
           : base(options)
        {
        }

        public DbSet<ToDoItem> ToDos { get; set; }
    }
}
