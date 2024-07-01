using messenger_server.Models.Entities;

// Add documentation
namespace messenger_server.Services.Interfaces
{
    public interface IUserService
    {
        public bool AddUser(User user);

        public bool CheckExistUser(User user);

        public bool CheckCorrectLogin(string login);

        public bool CheckCorrectPassword(string password);

        public bool CheckCorrectEmail(string email);

        public User? GetUserByLogin(string login);
    }
}
