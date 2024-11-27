using Airline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Airline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AircraftTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AircraftTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

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
            return RedirectToAction("Index");
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
            try
            {
                _context.Update(aircraftType);
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
            var aircraftType = await _context.AircraftTypes
                .Include(a => a.Aircrafts) 
                .FirstOrDefaultAsync(a => a.TypeId == id);

            if (aircraftType == null)
            {
                return NotFound();
            }

            if (aircraftType.Aircrafts != null && aircraftType.Aircrafts.Any())
            {
                return BadRequest("Нельзя удалить тип самолета, пока существуют самолеты на его основе.");
            }

            _context.AircraftTypes.Remove(aircraftType);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
