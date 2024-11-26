using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers
{
    public class PassengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
