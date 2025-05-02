using System;

namespace MessengerServer.Infrastructure.Models.Entities
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int RecipientId { get; set; }

        public DateTime DepartureDate { get; set; }

        public string Text { get; set; } = null!;
    }
}
