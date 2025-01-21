using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlanCare.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class CarRegistrationMonitorService : BackgroundService
{
    private readonly IHubContext<CarHub> _hubContext;
    private readonly ILogger<CarRegistrationMonitorService> _logger;

    private List<Car> _cars;

    public CarRegistrationMonitorService(IHubContext<CarHub> hubContext, ILogger<CarRegistrationMonitorService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;

        // Dummy data for cars
        _cars = new List<Car>
        {
            new Car { Id = 1, Make = "Toyota", Model = "Corolla", RegistrationExpiryDate = DateTime.Now.AddMonths(1), IsRegistrationValid = true },
            new Car { Id = 2, Make = "Honda", Model = "Civic", RegistrationExpiryDate = DateTime.Now.AddMonths(-1), IsRegistrationValid = false }
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Simulate checking car registration expiry status periodically (every minute)
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            _logger.LogInformation("Checking car registration expiry statuses...");

            // Check for registration expiry and notify clients if there's a change
            foreach (var car in _cars)
            {
                var currentStatus = car.IsRegistrationValid;
                if (car.RegistrationExpiryDate < DateTime.Now)
                {
                    car.IsRegistrationValid = false;
                }
                else
                {
                    car.IsRegistrationValid = true;
                }

                // If the status has changed, notify clients
                if (currentStatus != car.IsRegistrationValid)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveCarRegistrationStatusUpdate", car);
                    _logger.LogInformation($"Car {car.Make} {car.Model} registration status updated: {car.IsRegistrationValid}");
                }
            }
        }
    }
}
