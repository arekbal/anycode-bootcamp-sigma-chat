using Microsoft.AspNetCore.SignalR;
using Neuroglia.AsyncApi;
using Saunter;

namespace ChatAPI.Hubs;

[AsyncApi("Chat", "1.0.0")]
public class ChatHub : Hub
{


  [Channel("/ws/channels"), SubscribeOperation]
  public async Task JoinChannel(string connectionId, string channelName)
  {
    await Groups.AddToGroupAsync(connectionId, channelName);
  }
  public async Task NewMessage(string username, string message) =>
      await Clients.All.SendAsync("messageReceived", username, message);
}