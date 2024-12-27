using Microsoft.AspNetCore.SignalR;

namespace AracKiralamaPortal.Hubs
{
    public class CarHub : Hub
    {
        public async Task NotifyCarUpdated()
        {
            await Clients.All.SendAsync("CarUpdated");
        }
    }
}
