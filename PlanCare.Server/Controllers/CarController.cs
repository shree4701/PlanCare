using Microsoft.AspNetCore.Mvc;

namespace PlanCare.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        // Example list of cars (usually, you would get this from a database)
        private static readonly List<Car> cars =
        [
            new Car { Id = 1, Make = "Toyota", Model = "Corolla", Year = 2010, RegistrationExpiryDate = new DateTime(2026,04,30,0,0,0) },
            new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2011, RegistrationExpiryDate = new DateTime(2007,11,05,0,0,0) }
        ];

        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCars")]
        public IEnumerable<Car> Get([FromQuery] string? make)
        {
            // If 'make' is not provided, return all cars
            var filteredCars = string.IsNullOrEmpty(make)
                ? cars // No filter, return all cars
                : cars.Where(car => car.Make.Equals(make, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredCars;
        }
    }
}
