using System.Net.Sockets;

namespace Airline.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public int DepartureDestinationId { get; set; }
        public int ArrivalDestinationId { get; set; }
        public DateTime DepartureDatetime { get; set; }
        public DateTime ArrivalDatetime { get; set; }
        public int AircraftId { get; set; }
        public int CrewId { get; set; }
        public Destination DepartureDestination { get; set; }
        public Destination ArrivalDestination { get; set; }
        public Aircraft Aircraft { get; set; }
        public Crew Crew { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
