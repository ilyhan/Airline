using Airline.Models;
using Airline.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DestinationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestinationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _context.Destinations.ToListAsync();
            return View(destinations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Destination destination)
        {
            _context.Add(destination);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.FindAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Destination dest)
        {

            try
            {
                _context.Update(dest);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var dest = await _context.Destinations
                .Include(a => a.DepartureFlights)
                .Include(a => a.ArrivalFlights)
                .FirstOrDefaultAsync(a => a.DestinationId == id);

            if (dest == null)
            {
                return NotFound();
            }

            if (dest.ArrivalFlights.Any() || dest.DepartureFlights.Any())
            {
                return BadRequest("Нельзя удалить пункт, тк на него назначен полет");
            }

            _context.Destinations.Remove(dest);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
