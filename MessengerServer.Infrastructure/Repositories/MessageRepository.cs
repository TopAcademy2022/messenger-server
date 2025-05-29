using System;
using System.Collections.Generic;
using System.Linq;
using MessengerServer.Infrastructure.Models.Entities;
using MessengerServer.Infrastructure.Persistence;

namespace MessengerServer.Infrastructure.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        private AppDbContextBase dbConnection;

        public MessageRepository(AppDbContextBase dbConnection) : base(dbConnection)
        {
            this.dbConnection = dbConnection;
        }

		public bool Send(Message message)
        {
            try
            {
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
