namespace Airline.Models
{
    public class Crew
    {
        public int CrewId { get; set; }
        public string FlightZone { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
