using System;
using System.Collections.Generic;
using System.Linq;
using MessengerServer.Infrastructure.Models.Entities;
using MessengerServer.Infrastructure.Services;

namespace MessengerServer.Infrastructure.Repositories
{
    public class MessageRepository// : IMessageService
    {
        public bool Send(Message message)
        {
            try
            {
				DbConnection dbConnection = new DbConnection();

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
				DbConnection dbConnection = new DbConnection();

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
