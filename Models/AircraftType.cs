using System.ComponentModel.DataAnnotations;

namespace Airline.Models
{
    public class AircraftType
    {
        [Key]
        public int TypeId { get; set; }
        public string Brand { get; set; }
        public int Range { get; set; }
        public int PassengerCapacityEconomy { get; set; }
        public int PassengerCapacityBusiness { get; set; }
        public int MaxFlightAltitude { get; set; }
        public int CruiseSpeed { get; set; }
        public int MaxTakeoffWeight { get; set; }
        public ICollection<Aircraft> Aircrafts { get; set; }
    }
}
