namespace Airline.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }
        public ICollection<Flight> DepartureFlights { get; set; }
        public ICollection<Flight> ArrivalFlights { get; set; }
    }
}
