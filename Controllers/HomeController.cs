using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Airline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // Предположим, что у вас есть контекст базы данных

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchTickets(string from, string to, DateTime flightDate, string classType)
        {
            var flights = _context.Flights
                .Include(f => f.DepartureDestination)
                .Include(f => f.ArrivalDestination)
                .Include(f => f.Tickets)
                .Where(f => f.DepartureDestination.City == from &&
                             f.ArrivalDestination.City == to &&
                             f.DepartureDatetime.Date == flightDate.Date)
                .ToList();

            var tickets = flights.SelectMany(f => f.Tickets)
                .Where(t => t.Class == classType)
                .ToList();

            return View(tickets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
