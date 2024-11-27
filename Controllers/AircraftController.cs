using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Airline.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Airline.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Airline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AircraftController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AircraftController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var aircrafts = await _context.Aircrafts.Include(a => a.Type).ToListAsync();
            return View(aircrafts);
        }

        public async Task<IActionResult> Create()
        {
            var aircraftTypes = await _context.AircraftTypes.ToListAsync();
            var viewModel = new CreateAircraftViewModel
            {
                Aircraft = new Aircraft(), 
                AircraftTypes = aircraftTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAircraftViewModel model)
        {
            _context.Add(model.Aircraft);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts.FindAsync(id);
            var aircraftTypes = await _context.AircraftTypes.ToListAsync();

            var aircraftEdit = new CreateAircraftViewModel
            {
                Aircraft = aircraft,
                AircraftTypes = aircraftTypes
            };

            if (aircraftEdit == null)
            {
                return NotFound();
            }

            return View(aircraftEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAircraftViewModel model)
        {

            try
            {
                _context.Update(model.Aircraft);
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
            var aircraft = await _context.Aircrafts
                .Include(a => a.Flights)
                .FirstOrDefaultAsync(a => a.AircraftId == id);

            if (aircraft == null)
            {
                return NotFound();
            }

            if (aircraft.Flights != null && aircraft.Flights.Any())
            {
                return BadRequest("Нельзя удалить самолет, тк на него назначен полет");
            }

            _context.Aircrafts.Remove(aircraft);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
