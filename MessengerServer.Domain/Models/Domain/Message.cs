using System;

namespace MessengerServer.Domain.Models.Domain
{
    public class Message
    {
        public User Sender { get; set; }

        public User Recipient { get; set; }

        public DateTime DepartureDate { get; set; }

        public string Text { get; set; } = null!;
    }
}
