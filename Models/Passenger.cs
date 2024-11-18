using System.Net.Sockets;

namespace Airline.Models
{
    public class Passenger
    {
        public int PassengerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }
        public string? Passport { get; set; }
        public DateTime? BirthDate { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

        public string? ApplicationUserId { get; set; } 
        public ApplicationUser? ApplicationUser { get; set; } 
    }
}
