using messenger_server.Models.Entities;

namespace messenger_server.Services.Interfaces
{
    public interface IMessageService
    {
        public bool Send(Message message);

        public List<Message> Get(User recipient);
    }
}