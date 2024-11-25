namespace Airline.Models.ViewModels
{
    public class FlightTicketsViewModel
    {
        public int FlightId { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
