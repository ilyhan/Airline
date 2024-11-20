using Airline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace Airline.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; 

        public HomeController(UserManager<ApplicationUser> userManager, ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchTickets(string from, string to, DateTime flightDate, string classType)
        {
            Console.WriteLine(to);
            var flights = _context.Flights
                .Include(f => f.DepartureDestination)
                .Include(f => f.ArrivalDestination)
                .Include(f => f.Tickets)
                .Where(f => f.DepartureDestination.City == from &&
                             f.ArrivalDestination.City == to &&
                             f.DepartureDatetime.Date == flightDate.Date)
                .ToList();

            var tickets = flights.SelectMany(f => f.Tickets)
                .Where(t => t.Class == classType &&
                            t.PassengerId == null) 
                .ToList();

            return View(tickets);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Ticketing(int id)
        {
            var email = User.Identity.Name;

            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            Passenger passenger = null;

            if (user != null)
            {
                passenger = _context.Passengers.FirstOrDefault(p => p.ApplicationUserId == user.Id);
            }

            if(passenger == null)
            {
                return View("Login");
            }

            var ticket = _context.Tickets
                .Include(t => t.Flight)
                .ThenInclude(f => f.DepartureDestination)
                .Include(t => t.Flight.ArrivalDestination)
                .FirstOrDefault(t => t.TicketId == id);

            if (ticket != null)
            {
                var ticketingInfo = new TicketingViewModel
                {
                    Ticket = ticket,
                    Passenger = passenger
                };

                return View(ticketingInfo); 
            }
            return View("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SuccesTicketing(string name, string surname, string? patronymic, DateTime birthDate, string passport, int ticketId)
        {
            Console.WriteLine(ticketId);

            var email = User.Identity.Name;

            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            Passenger passenger = null;

            if (user != null)
            {
                passenger = _context.Passengers.FirstOrDefault(p => p.ApplicationUserId == user.Id);
            }

            if (passenger != null)
            {
                if (string.IsNullOrEmpty(passenger.Passport) || passenger.BirthDate == null)
                {
                    if (!string.IsNullOrEmpty(passport))
                    {
                        passenger.Passport = passport;
                    }
                    if (passenger.BirthDate == null)
                    {
                        passenger.BirthDate = birthDate;
                    }
                    if(patronymic != null && string.IsNullOrEmpty(passenger.Patronymic))
                    {
                        passenger.Patronymic = patronymic;
                    }

                    _context.Passengers.Update(passenger);
                    _context.SaveChanges(); 
                }

                var ticket = _context.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
                
                if (ticket != null)
                {
                    ticket.PassengerId = passenger.PassengerId;
                    _context.Tickets.Update(ticket); 
                    _context.SaveChanges(); 
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }

            return View("Index");
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
