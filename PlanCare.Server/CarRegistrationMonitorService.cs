using Microsoft.AspNetCore.SignalR;
using PlanCare.Server;

public class CarRegistrationMonitorService : BackgroundService
{
    private readonly IHubContext<CarRegistrationHub> _hubContext;
    private readonly ILogger<CarRegistrationMonitorService> _logger;

    private List<Car> _cars;

    public CarRegistrationMonitorService(IHubContext<CarRegistrationHub> hubContext, ILogger<CarRegistrationMonitorService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;

        // Dummy data for cars
        _cars = new List<Car>
        {
            new Car { Id = 1, Make = "Toyota", Model = "Corolla", RegistrationExpiryDate = DateTime.Now.AddDays(-1) },
            new Car { Id = 2, Make = "Honda", Model = "Civic", RegistrationExpiryDate = DateTime.Now.AddDays(2) },
            new Car { Id = 3, Make = "Toyota", Model = "Prado", RegistrationExpiryDate = DateTime.Now.AddDays(0) },
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var car in _cars)
            {
                bool isExpired = car.IsExpired;
                await _hubContext.Clients.All.SendAsync("ReceiveCarRegistrationStatus", car.Id, isExpired);
            }

            await Task.Delay(10000, stoppingToken); // Delay for 10 seconds before checking again
        }
    }
}
