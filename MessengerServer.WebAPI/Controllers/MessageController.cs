using Microsoft.AspNetCore.Mvc;
using MessengerServer.Infrastructure.Repositories;
using MessengerServer.Infrastructure.Models.Entities;
using MessengerServer.Infrastructure.Persistence;
using MessengerServer.Domain.Repositories;

namespace MessengerServer.Controllers
{
    [ApiController]
    [Route("messenger")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

		private readonly IRepository<Message> _messageRepository;

        private readonly UserRepository _userRepository;

        public MessageController(AppDbContextBase dbConnection)
        {
            //this._logger = logger;
            this._messageRepository = new BaseRepository<Message>(dbConnection);
            this._userRepository = new UserRepository(dbConnection);
        }

        [HttpPost]
        public bool PostMessage(string loginSender, string loginRecipient, string messageText)
        {
            Message message = new Message()
            {
                SenderId = this._userRepository.GetUserByLogin(loginSender).Id,
                RecipientId = this._userRepository.GetUserByLogin(loginRecipient).Id,
                DepartureDate = new DateTime(),
                Text = messageText
            };

            // TODO: Rewrite
            if (message.SenderId != null && message.RecipientId != null)
            {
                if (this._messageRepository.Add(message))
                {
                    return true;
                }
            }
            
            return false;
        }

        [HttpGet]
        public IEnumerable<Message> GetMessage(string loginRecipient)
        {
            User recipient = this._userRepository.GetUserByLogin(loginRecipient);
			List<Message> messages = this._messageRepository.GetAll().Where(m => m.RecipientId == recipient.Id).ToList();
            
            return messages;
        }
    }
}
