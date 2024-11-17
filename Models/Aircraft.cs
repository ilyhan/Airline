namespace Airline.Models
{
    public class Aircraft
    {
        public int AircraftId { get; set; }
        public string TailNumber { get; set; }
        public int OperationalPeriod { get; set; }
        public int YearOfManufacture { get; set; }
        public int TypeId { get; set; }
        public AircraftType Type { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
