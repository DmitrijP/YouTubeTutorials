namespace EmployeeManagementSystem.Data.Shared.Entities
{
    public class Location
    {
        public int? Id { get; set; }
        public int? StreetId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
    }
}
