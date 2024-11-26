namespace Airline.Models.ViewModels
{
    public class CreateAircraftViewModel
    {
        public Aircraft? Aircraft { get; set; }
        public IEnumerable<AircraftType>? AircraftTypes { get; set; }
    }
}
