using Microsoft.AspNetCore.Mvc;
using messenger_server.Services.Interfaces;
using messenger_server.Models.Entities;
using messenger_server.Services;

namespace messenger_server.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private readonly IUserServise _userServise;

        public LoginController(ILogger<RegistrationController> logger)
        {
            this._logger = logger;
            this._userServise = new UserService();
        }

        [HttpGet]
        public bool GetUser(string login, string password)
        {
            // Check data is correct
            if (this._userServise.CheckCorrectLogin(login) &&
                this._userServise.CheckCorrectPassword(password))
            {
                User newUser = new User()
                {
                    Login = login,
                    Password = password
                };

                if (this._userServise.CheckExistUser(newUser))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
