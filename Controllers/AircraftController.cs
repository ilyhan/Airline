using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Airline.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Airline.Models.ViewModels;

namespace Airline.Controllers
{
    public class AircraftController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AircraftController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index()
        {
            var aircrafts = await _context.Aircrafts.Include(a => a.Type).ToListAsync();
            return View(aircrafts);
        }

        // GET: Aircraft/Create
        public async Task<IActionResult> Create()
        {
            var aircraftTypes = await _context.AircraftTypes.ToListAsync();
            var viewModel = new CreateAircraftViewModel
            {
                Aircraft = new Aircraft(), // Initialize an empty Aircraft object
                AircraftTypes = aircraftTypes
            };

            return View(viewModel);
        }

        // POST: Aircraft/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateAircraftViewModel model)
        {
            _context.Add(model.Aircraft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Aircraft/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            ViewData["TypeId"] = new SelectList(await _context.AircraftTypes.ToListAsync(), "TypeId", "TypeName", aircraft.TypeId);
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Aircraft aircraft)
        {
            if (id != aircraft.AircraftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.AircraftId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(await _context.AircraftTypes.ToListAsync(), "TypeId", "TypeName", aircraft.TypeId);
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            _context.Aircrafts.Remove(aircraft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircrafts.Any(e => e.AircraftId == id);
        }
    }
}
