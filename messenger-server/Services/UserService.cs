using System.Text.RegularExpressions;
using messenger_server.Models.Entities;
using messenger_server.Services.Interfaces;

// Add documentation
namespace messenger_server.Services
{
	public class UserService : IUserService
	{
		private readonly ILogger<UserService> _logger;

		public UserService(ILogger<UserService> logger)
		{
			this._logger = logger;
		}

		public bool AddUser(User user)
		{
			try
			{
				DatabaseConnection dbConnection = new DatabaseConnection();

				if (this.CheckUniqUser(user))
				{
					dbConnection.Users.Add(user);
					dbConnection.SaveChanges();

					return true;
				}
			}
			catch (Exception exception)
			{
				this._logger.LogError(exception.Message);
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
				DatabaseConnection dbConnection = new DatabaseConnection();

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
				this._logger.LogError(exception.Message);
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
			catch (Exception exception)
			{
				this._logger.LogError(exception.Message);
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
			catch (Exception exception)
			{
				this._logger.LogError(exception.Message);
			}

			return null;
		}
	}
}