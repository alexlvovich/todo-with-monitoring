using System.ComponentModel.DataAnnotations;

namespace todo.Abstractions
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
