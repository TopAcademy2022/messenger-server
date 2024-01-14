using messanger_server.Models;
using messanger_server.Services.Interfaces;

// Add documentation
namespace messanger_server.Services
{
    public class UserService : IUserServise
    {
        public bool AddUser(User user)
        {
            try
            {
                // Replace
                DatabaseConnection dbConnection = new DatabaseConnection();

                // Replace
                if (!dbConnection.Users.Contains(user))
                {
                    dbConnection.Users.Add(user);
                    dbConnection.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Replace
            }

            return false;
        }

        // Replace
        public bool CheckCorrectLogin(string login)
        {
            return true;
        }

        // Replace
        public bool CheckCorrectPassword(string password)
        {
            return true;
        }

        // Replace
        public bool CheckCorrectEmail(string email)
        {
            return true;
        }
    }
}
