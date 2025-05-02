using System.Collections.Generic;
using MessengerServer.Domain.Models.Domain;

namespace MessengerServer.Domain.Services
{
    public interface IMessageService
    {
        public bool Send(Message message);

        public List<Message> Get(User recipient);
    }
}
