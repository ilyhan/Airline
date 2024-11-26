using Airline.Models.ViewModels;
using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.Controllers
{
    public class FlightController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var flights = await _context.Flights
                .Include(a => a.Aircraft)
                .Include(d => d.ArrivalDestination)
                .Include(d => d.DepartureDestination)
                .ToListAsync();
            return View(flights);
        }

        public async Task<IActionResult> Create()
        {
            var destination = await _context.Destinations.ToListAsync();
            var aircraft = await _context.Aircrafts.ToListAsync();

            var viewModel = new CreateFlightModel
            {
                Flight = new Flight(),
                Destination = destination,
                Aircraft = aircraft,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFlightModel model)
        {
            _context.Add(model.Flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
