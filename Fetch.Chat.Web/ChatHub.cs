using Fetch.Chat.Domain;
using Microsoft.AspNetCore.SignalR;

namespace Fetch.Chat.Web
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(string user, string message)
        {
            _chatService.AddMessage(new(user, message));
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task<IReadOnlyCollection<Message>> GetAllMessages()
        {
            return Task.FromResult(_chatService.GetAllMessages());
        }
    }
}