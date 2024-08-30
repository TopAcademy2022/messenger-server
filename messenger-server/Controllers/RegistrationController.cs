using Microsoft.AspNetCore.Mvc;
using messenger_server.Services;
using messenger_server.Services.Interfaces;
using messenger_server.Models.Entities;

// Add documentation
namespace messenger_server.Controllers
{
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private readonly IUserService _userService;

        public RegistrationController(ILogger<RegistrationController> logger, ILogger<UserService> userServiceLogger)
        {
            this._logger = logger;
            this._userService = new UserService(userServiceLogger);
        }

        [HttpPost]
        public string Post(string login, string password, string email)
        {
            const string SUCCESS = "successful registration";
            const string FAILED = "failed registration";

            // Check data is correct
            if (this._userService.CheckCorrectLogin(login) &&
                this._userService.CheckCorrectPassword(password) &&
                this._userService.CheckCorrectEmail(email))
            {
                User newUser = new User()
                {
                    Login = login,
                    Password = password,
                    Email = email
                };

                if (this._userService.AddUser(newUser))
                {
                    return SUCCESS;
                }

                return FAILED;
            }

            return FAILED;
        }
    }
}