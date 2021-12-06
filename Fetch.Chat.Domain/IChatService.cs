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

        public void AddSportMessage(Message message);

        public IReadOnlyCollection<Message> GetMessages();

        public IReadOnlyCollection<Message> GetSportMessages();
    }
}