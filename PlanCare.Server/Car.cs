namespace PlanCare.Server
{

    public class Car
    {
        public int Id { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public int Year { get; set; }
        public DateTime RegistrationExpiryDate { get; set; }
        public bool IsRegistrationValid { get; set; }
    }
}
