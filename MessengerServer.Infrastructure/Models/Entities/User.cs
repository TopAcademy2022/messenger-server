// TODO: Add documentation
namespace MessengerServer.Infrastructure.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
