using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PassengerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var passengers = _context.Passengers.Include(f=>f.Tickets).ToList();
            return View(passengers);
        }
    }
}
