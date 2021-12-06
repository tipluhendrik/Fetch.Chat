using Fetch.Chat.Domain;
using Microsoft.AspNetCore.SignalR;

namespace Fetch.Chat.Web
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private const string sportChannel = "sport";
        private const string mainChannel = "main";
        private const string receiveMessageEvent = "ReceiveMessage";

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, mainChannel);
            await base.OnConnectedAsync();
        }

        public async Task<IReadOnlyCollection<Message>> JoinSport()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, mainChannel);
            await Groups.AddToGroupAsync(Context.ConnectionId, sportChannel);
            return _chatService.GetSportMessages();
        }

        public async Task SendMessage(string user, string message)
        {
            var msg = new Message(Guid.NewGuid(), user, message);
            _chatService.AddMessage(msg);
            await Clients.Groups(mainChannel).SendAsync(receiveMessageEvent, msg);
        }

        public async Task SendMessageToSport(string user, string message)
        {
            var msg = new Message(Guid.NewGuid(), user, message);
            _chatService.AddSportMessage(msg);
            await Clients.Groups(sportChannel).SendAsync(receiveMessageEvent, msg);
        }

        public Task<IReadOnlyCollection<Message>> GetAllMessages()
        {
            return Task.FromResult(_chatService.GetMessages());
        }

        public Task<IReadOnlyCollection<Message>> GetAllSportMessages()
        {
            return Task.FromResult(_chatService.GetSportMessages());
        }
    }
}