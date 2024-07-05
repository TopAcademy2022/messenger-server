using messenger_server.Models.Entities;
using messenger_server.Services.Interfaces;

namespace messenger_server.Services
{
    public class MessageService : IMessageService
    {
        public bool Send(Message message)
        {
            try
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                dbConnection.Messages.Add(message);
                dbConnection.SaveChanges();

                return true;
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Database connection error: {exeption.Message}");
            }

            return false;
        }

        public List<Message> Get(User recipient)
        {
            List<Message> allMessangesForUser = new List<Message>();

            try
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                allMessangesForUser = dbConnection.Messages
                    .Where(m => m.RecipientId == recipient.Id)
                    .ToList();
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Database connection error: {exeption.Message}");
            }

            return allMessangesForUser;
        }
    }
}
