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

        public async Task<IReadOnlyCollection<Message>> JoinSport()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "sport");
            return _chatService.GetSportMessages();
        }

        public async Task LeaveSport()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "sport");
        }

        public async Task SendMessage(string user, string message)
        {
            _chatService.AddMessage(new(user, message));
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToSport(string user, string message)
        {
            _chatService.AddMessage(new(user, message));
            await Clients.Groups("sport").SendAsync("ReveiceMessage", user, message);
        }

        public Task<IReadOnlyCollection<Message>> GetAllMessages()
        {
            return Task.FromResult(_chatService.GetMessages());
        }
    }
}