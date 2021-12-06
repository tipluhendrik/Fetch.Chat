using Fetch.Chat.Domain;
using Microsoft.AspNetCore.SignalR;

namespace Fetch.Chat.Web
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private const string sportChannel = "sport";
        private const string receiveMessageEvent = "ReceiveMessage";

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task<IReadOnlyCollection<Message>> JoinSport()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, sportChannel);
            return _chatService.GetSportMessages();
        }

        public async Task LeaveSport()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, sportChannel);
        }

        public async Task SendMessage(string user, string message)
        {
            _chatService.AddMessage(new(Guid.NewGuid(), user, message));
            await Clients.All.SendAsync(receiveMessageEvent, user, message);
        }

        public async Task SendMessageToSport(string user, string message)
        {
            _chatService.AddMessage(new(Guid.NewGuid(), user, message));
            await Clients.Groups(sportChannel).SendAsync(receiveMessageEvent, user, message);
        }

        public Task<IReadOnlyCollection<Message>> GetAllMessages()
        {
            return Task.FromResult(_chatService.GetMessages());
        }
    }
}