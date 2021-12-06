using Fetch.Chat.Domain;

namespace Fetch.Chat.Infrastructure
{
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