using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace signalr.Hubs
{
    public class TermHub : Hub
    {
        private static Dictionary<string, string> conexoes = new Dictionary<string, string>();
        public async Task SendMessage(string id, string message)
        {
            if(id.StartsWith("id="))
            {
                conexoes[id.Substring(3)] = Context.ConnectionId;
                await Clients. Caller.SendAsync("ReceiveMessage", "Registrado", id);
            }else
            {
                await Clients.Client(conexoes[id]).SendAsync("ReceiveMessage", id, message);
            }
        }

    }
}