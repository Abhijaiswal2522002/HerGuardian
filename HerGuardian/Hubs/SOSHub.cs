using Microsoft.AspNetCore.SignalR;
namespace HerGuardian.Hubs
{
    public class SOSHub:Hub
    {
        public async Task SendAlert(string message)
        {
            await Clients.All.SendAsync("ReceiveAlert", message);
        }


    }
}
