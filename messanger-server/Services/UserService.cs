using System.Text.RegularExpressions;
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
    }
}
