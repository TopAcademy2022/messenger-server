using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using MessengerServer.Infrastructure.Models.Entities;
using MessengerServer.Infrastructure.Services;

// TODO: Add documentation
namespace MessengerServer.Infrastructure.Repositories
{
    public class UserRepository// : IUserService
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger)
        {
            _logger = logger;
        }

        public bool AddUser(User user)
        {
            try
            {
				DbConnection dbConnection = new DbConnection();

                if (CheckUniqUser(user))
                {
                    dbConnection.Users.Add(user);
                    dbConnection.SaveChanges();

                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            return false;
        }

        /*!
		* @brief That checks the user's name for uniqueness
		* @param[in] user A new user who is being checked for uniqueness
		* @return True - the user is not found by username. False - The user is not unique.
		*/
        private bool CheckUniqUser(User user)
        {
            try
            {
				DbConnection dbConnection = new DbConnection();

                /*!
				* @if
				* Сhecking the user for uniqueness by login
				* @endif
				*/
                if (!dbConnection.Users.Any(u => u.Login == user.Login))
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            return false;
        }

        public bool CheckExistUser(User user)
        {
            try
            {
				DbConnection dbConnection = new DbConnection();

                List<User>? gettingUsers = dbConnection.Users
                    .Where(u => u.Login == user.Login && u.Password == user.Password)
                    .ToList();

                if (gettingUsers.Count() > 0)
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
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
				DbConnection dbConnection = new DbConnection();

                List<User>? gettingUsers = dbConnection.Users.Where(u => u.Login == login)
                    .ToList();

                if (gettingUsers.Count > 0)
                {
                    return gettingUsers.First();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            return null;
        }
    }
}