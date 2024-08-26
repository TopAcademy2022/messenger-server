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

        private readonly IUserService _userService;

        public LoginController(ILogger<RegistrationController> logger, ILogger<UserService> userServiceLogger)
        {
            this._logger = logger;
            this._userService = new UserService(userServiceLogger);
        }

        [HttpGet]
        public bool GetUser(string login, string password)
        {
            // Check data is correct
            if (this._userService.CheckCorrectLogin(login) &&
                this._userService.CheckCorrectPassword(password))
            {
                User newUser = new User()
                {
                    Login = login,
                    Password = password
                };

                if (this._userService.CheckExistUser(newUser))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
