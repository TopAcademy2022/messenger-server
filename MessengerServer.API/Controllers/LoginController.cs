using Microsoft.AspNetCore.Mvc;
using MessengerServer.Infrastructure.Repositories;
using MessengerServer.Infrastructure.Models.Entities;

namespace MessengerServer.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private readonly UserRepository _userService;

        public LoginController(ILogger<RegistrationController> logger, ILogger<UserRepository> userServiceLogger)
        {
            this._logger = logger;
            this._userService = new UserRepository(userServiceLogger);
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
