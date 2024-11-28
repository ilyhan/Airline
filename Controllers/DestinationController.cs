using Airline.DAL;
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
        private readonly DestinationDBstorage _destinationDBstorage;

        public DestinationController(ApplicationDbContext context)
        {
            _destinationDBstorage = new DestinationDBstorage(context);
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _destinationDBstorage.getAllDestinations();
            return View(destinations);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Destination destination)
        {
            _destinationDBstorage.AddDestination(destination);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _destinationDBstorage.FindById(id);

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
               await _destinationDBstorage.Update(dest);
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
            await _destinationDBstorage.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}
