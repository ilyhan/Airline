namespace Airline.Models.ViewModels
{
    public class CreateFlightModel
    {
        public Flight? Flight { get; set; }
        public IEnumerable<Destination>? Destination { get; set; }
        public IEnumerable<Aircraft>? Aircraft { get; set; }

    }
}
