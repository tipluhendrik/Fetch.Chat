using Fetch.Chat.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Fetch.Chat.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatRoomController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet(Name = "messages")]
        public IReadOnlyCollection<Message> GetAllMessages()
        {
            return _chatService.GetAllMessages();
        }

        [HttpPost(Name = "sendmessage")]
        public IActionResult SendMessage(Message message)
        {
            _chatService.AddMessage(message);
            return Ok();
        }
    }
}