using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers
{
    public class AircraftTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AircraftTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AircraftTypes
        public async Task<IActionResult> Index()
        {
            var aircraftTypes = await _context.AircraftTypes.ToListAsync();
            return View(aircraftTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AircraftType aircraftType)
        {
            _context.Add(aircraftType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftType = await _context.AircraftTypes.FindAsync(id);
            if (aircraftType == null)
            {
                return NotFound();
            }
            return View(aircraftType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AircraftType aircraftType)
        {
            if (id != aircraftType.TypeId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(aircraftType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AircraftTypeExists(aircraftType.TypeId))
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

        private bool AircraftTypeExists(int id)
        {
            return _context.AircraftTypes.Any(e => e.TypeId == id);
        }
    }
}
