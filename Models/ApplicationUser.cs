using Microsoft.AspNetCore.Identity;

namespace Airline.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Passenger Passenger { get; set; } 
    }

}
