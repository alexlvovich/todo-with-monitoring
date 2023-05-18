using Konso.Clients.ValueTracking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using todo.Abstractions;
using todo.Data;
using todo.Models;

namespace todo.Pages
{
    public class EditModel : KonsoModel
    {
        private readonly ToDoDbContext _context;

        public EditModel(ToDoDbContext context, IValueTrackingClient valueTrackingService) : base(valueTrackingService)
        {
            _context = context;
        }

        [BindProperty]
        public ToDoItem ToDo { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ToDo = await _context.ToDos.FindAsync(id);

            if (ToDo == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ToDo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                await TrackValue("test@user.com", ValueTrackingTypes.UpdateTask);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"ToDo item {ToDo.Id} not found!");
            }

            return RedirectToPage("./Index");
        }
    }
}
