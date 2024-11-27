using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t=>t.Flight)
                .Include(t=>t.Passenger)
                .ToListAsync();

            if(tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }
    }
}
