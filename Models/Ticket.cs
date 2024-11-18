namespace Airline.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int FlightId { get; set; }
        public decimal Price { get; set; }
        public int? RowNumber { get; set; }
        public int? SeatNumber { get; set; }
        public string Class { get; set; }

        public int? PassengerId { get; set; }
        public string BaggageType { get; set; }
        public decimal BaggageWeight { get; set; }

        public Flight? Flight { get; set; }
        public Passenger? Passenger { get; set; }
    }
}
