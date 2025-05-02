using Microsoft.AspNetCore.Mvc;
using MessengerServer.Infrastructure.Repositories;
using MessengerServer.Infrastructure.Models.Entities;

namespace MessengerServer.Controllers
{
    [ApiController]
    [Route("messenger")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private readonly MessageRepository _messageService;

        private readonly UserRepository _userService;

        public MessageController(ILogger<RegistrationController> logger, ILogger<UserRepository> userServiceLogger)
        {
            this._logger = logger;
            this._messageService = new MessageRepository();
            this._userService = new UserRepository(userServiceLogger);
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

            // TODO: Rewrite
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
        public IEnumerable<Message> GetMessage(string loginRecipient)
        {
            List<Message> messages = this._messageService.Get(this._userService.GetUserByLogin(loginRecipient));
            
            return messages;
        }
    }
}
