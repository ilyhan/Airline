namespace Airline.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Passport { get; set; }
        public string Phone { get; set; }
        public string AddressCountry { get; set; }
        public string AddressCity { get; set; }
        public string AddressStreet { get; set; }
        public string AddressHouse { get; set; }
        public string AddressApartment { get; set; }
        public string Position { get; set; }
        public int ExperienceYears { get; set; }
        public int CrewId { get; set; }
        public Crew Crew { get; set; }
    }
}
