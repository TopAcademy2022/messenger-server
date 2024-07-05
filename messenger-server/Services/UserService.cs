using System.Text.RegularExpressions;
using messenger_server.Models.Entities;
using messenger_server.Services.Interfaces;

// Add documentation
namespace messenger_server.Services
{
    public class UserService : IUserService
    {
        public bool AddUser(User user)
        {
            try
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                // Replace
                if (!dbConnection.Users.Contains(user))
                {
                    dbConnection.Users.Add(user);
                    dbConnection.SaveChanges();

                    return true;
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Database connection error: {exeption.Message}");
            }

            return false;
        }

        public bool CheckExistUser(User user)
        {
            try
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                List<User>? gettingUsers = dbConnection.Users
                    .Where(u => u.Login == user.Login && u.Password == user.Password)
                    .ToList();

                if (gettingUsers.Count() > 0)
                {
                    return true;
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Database connection error: {exeption.Message}");
            }

            return false;
        }

        // Replace
        public bool CheckCorrectLogin(string login)
        {
            // Проверка длины
            if (string.IsNullOrEmpty(login) || login.Length > 20) /*Если логин пустой и его длина больше 20 то выводит false*/
            {
                return false;
            }

            //  Проверка на SQL-инъекции с помощью спец символов
            string sqlInjectionPattern = @"[\?&$'|#№/\\*;]+";
            if (Regex.IsMatch(login, sqlInjectionPattern))
            {
                return false;
            }

            // Проверка на XSS-инъекции с помощью спец символов
            string xssPattern = @"[<|>]+";
            if (Regex.IsMatch(login, xssPattern))
            {
                return false;
            }

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

        public User? GetUserByLogin(string login)
        {
            try
            {
                DatabaseConnection dbConnection = new DatabaseConnection();

                List<User>? gettingUsers = dbConnection.Users.Where(u => u.Login == login)
                    .ToList();

                if (gettingUsers.Count > 0)
                {
                    return gettingUsers.First();
                }
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Database connection error: {exeption.Message}");
            }

            return null;
        }
    }
}
