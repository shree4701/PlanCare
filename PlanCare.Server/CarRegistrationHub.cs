using Microsoft.AspNetCore.SignalR;
namespace PlanCare.Server
{
    public class CarRegistrationHub : Hub
    {
        public async Task SendCarRegistrationStatus(string carId, bool isExpired)
        {
            await Clients.All.SendAsync("ReceiveCarRegistrationStatus", carId, isExpired);
        }
    }
}
