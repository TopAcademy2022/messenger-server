using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using messenger_server.Models.Entities;
using messenger_server.Services.Interfaces;
using messenger_server.Services;

namespace messenger_server.Controllers
{
    [ApiController]
    [Route("messenger")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private readonly IMessageService _messageService;

        private readonly IUserService _userService;

        public MessageController(ILogger<RegistrationController> logger)
        {
            this._logger = logger;
            this._messageService = new MessageService();
            this._userService = new UserService();
        }

        [HttpPost]
        public bool PostMessage(string loginSender, string loginRecipient, string messageText)
        {
            Message message = new Message()
            {
                SenderId = this._userService.GetUserByLogin(loginSender).Id,
                RecipientId = this._userService.GetUserByLogin(loginRecipient).Id,
                DepartureDate = new DateTime(),
                Text = messageText
            };

            if (message.SenderId != null && message.RecipientId != null)
            {
                if (this._messageService.Send(message))
                {
                    return true;
                }
            }
            
            return false;
        }

        [HttpGet]
        public string GetMessage(string loginRecipient)
        {
            List<Message> messages = this._messageService.Get(this._userService.GetUserByLogin(loginRecipient));
            
            return JsonSerializer.Serialize(messages);
        }
    }
}
