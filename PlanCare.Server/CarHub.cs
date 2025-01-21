using Microsoft.AspNetCore.SignalR;
namespace PlanCare.Server
{
    public class CarHub : Hub
    {
        public async Task SendCarRegistrationStatusUpdate(Car car)
        {
            // Send the updated registration status to all connected clients
            await Clients.All.SendAsync("ReceiveCarRegistrationStatusUpdate", car);
        }
    }
}
