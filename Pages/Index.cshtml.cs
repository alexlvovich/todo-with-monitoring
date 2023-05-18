using Konso.Clients.ValueTracking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Abstractions;
using todo.Data;
using todo.Models;

namespace todo.Pages;

public class IndexModel : KonsoModel
{
    private readonly ToDoDbContext _context;
    private readonly ILogger _logger;
    public IndexModel(ToDoDbContext context, ILogger<IndexModel> logger, IValueTrackingClient valueTrackingService) : base(valueTrackingService)
    {
        _context = context;
        _logger = logger;
    }
    public IList<ToDoItem> ToDos { get; set; }
    public async Task OnGetAsync()
    {
        _logger.LogInformation("Getting todo list");
        ToDos = await _context.ToDos.ToListAsync();
        _logger.LogInformation($"Todo list {ToDos.Count} items");
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var t = await _context.ToDos.FindAsync(id);

        if (t != null)
        {
            _context.ToDos.Remove(t);
            await _context.SaveChangesAsync();

            await TrackValue("test@user.com", ValueTrackingTypes.DeleteTask);
        }

        return RedirectToPage();
    }
}
