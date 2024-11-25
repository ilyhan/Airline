using Airline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var destinations = _context.Destinations.ToList();
            return View(destinations);
        }

        public IActionResult AllPassenger()
        {
            var flights = _context.Flights
                .Include(f => f.DepartureDestination)
                .Include(f => f.ArrivalDestination)
                .Include(f => f.Tickets)
                .ToList();

            var tickets = flights.SelectMany(f => f.Tickets)
                .ToList();

            return View(tickets);
        }
    }
}
