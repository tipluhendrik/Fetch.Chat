using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fetch.Chat.Domain
{
    public interface IChatService
    {
        public void AddMessage(Message message);

        public IReadOnlyCollection<Message> GetAllMessages();
    }

    public record Message(string User, string Content);
}