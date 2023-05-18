using Konso.Clients.ValueTracking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using todo.Abstractions;
using todo.Data;
using todo.Models;

namespace todo.Pages
{
    public class CreateModel : KonsoModel
    {
        private readonly ToDoDbContext _context;
        private readonly ILogger _logger;

        public CreateModel(ToDoDbContext context, ILogger<CreateModel> logger, IValueTrackingClient valueTrackingService) : base(valueTrackingService)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ToDoItem ToDo{ get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Calling Post method");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model is invalid");
                return Page();
            }
            _logger.LogWarning($"Warning");
            _logger.LogError($"Error");
            _logger.LogDebug($"Debug");
            _logger.LogCritical($"Critical");
            _logger.LogTrace($"Trace");
            //_logger.LogDebug($"Todo object {JsonSerializer.Serialize(ToDo)}");
            _context.ToDos.Add(ToDo);
            _logger.LogInformation("Saving to database");
            await _context.SaveChangesAsync();
            await TrackValue("test@user.com", ValueTrackingTypes.CreateTask);
            _logger.LogInformation("Ending Post method");
            return RedirectToPage("./Index");
        }
    }
}
