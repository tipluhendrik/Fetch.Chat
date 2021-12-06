using Fetch.Chat.Domain;
using Fetchchat;
using Grpc.Core;
using static Fetchchat.ChatRoom;

namespace Fetch.Chat.Infrastructure
{
    public class ChatRoomService : ChatRoomBase
    {
        private IChatService _chatService;

        public ChatRoomService(IChatService chatService)
        {
            _chatService = chatService;
        }

        public override Task<ChatResponse> Send(ChatMessage request, ServerCallContext context)
        {
            Console.WriteLine($"New Message: {request}");

            var message = new Message(request.User, request.Message);
            _chatService.AddMessage(message);
            return Task.FromResult(new ChatResponse()
            {
                Success = true,
                UnixTime = DateTimeOffset.Now.ToUnixTimeSeconds()
            });
        }
    }

    public class ChatService : IChatService
    {
        private IList<Message> _chatMessages = new List<Message>();
        private IList<Message> sportMessages = new List<Message>();

        public void AddMessage(Message message)
        {
            _chatMessages.Add(message);
        }

        public void AddSportMessage(Message message)
        {
            sportMessages.Add(message);
        }

        public IReadOnlyCollection<Message> GetMessages()
        {
            return _chatMessages.ToArray();
        }

        public IReadOnlyCollection<Message> GetSportMessages()
        {
            return sportMessages.ToArray();
        }
    }
}