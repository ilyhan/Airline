using Airline.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Airline.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context; 

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var passenger = new Passenger
                    {
                        FirstName = model.Name,
                        LastName = model.Surname,
                        ApplicationUserId = user.Id
                    };
                    _context.Passengers.Add(passenger);
                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            var email = User.Identity.Name;

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _userManager.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            var passenger = _context.Passengers.FirstOrDefault(p => p.ApplicationUserId == user.Id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        public IActionResult Tickets(int id)
        {
            var flights = _context.Flights
                .Include(f => f.DepartureDestination)
                .Include(f => f.ArrivalDestination)
                .Include(f => f.Tickets)
                .ToList();

            var tickets = flights.SelectMany(f => f.Tickets)
                .Where(t => t.PassengerId == id)
                .ToList();

            if(tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        public IActionResult Edit(int id)
        {
            var passenger = _context.Passengers.FirstOrDefault(p => p.PassengerId == id);
            if (passenger == null) { 
                return NotFound();
            }
            return View(passenger);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Passenger passenger, IFormFile avatar)
        {
            var existingPassenger = await _context.Passengers.FindAsync(id);
            if (existingPassenger == null)
            {
                return NotFound();
            }

            existingPassenger.LastName = passenger.LastName;
            existingPassenger.FirstName = passenger.FirstName;
            existingPassenger.Patronymic = passenger.Patronymic;
            existingPassenger.BirthDate = passenger.BirthDate;

            if (avatar != null)
            {
                byte[] p1 = null;
                using (var fs1 = avatar.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    await fs1.CopyToAsync(ms1);
                    p1 = ms1.ToArray();
                }
                existingPassenger.Photo = p1;
            }

            _context.Passengers.Update(existingPassenger);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }

}
