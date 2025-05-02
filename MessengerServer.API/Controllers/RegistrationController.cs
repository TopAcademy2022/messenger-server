using Microsoft.AspNetCore.Mvc;
using MessengerServer.Infrastructure.Repositories;
using MessengerServer.Infrastructure.Models.Entities;

// Add documentation
namespace MessengerServer.Controllers
{
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private readonly UserRepository _userService;

        public RegistrationController(ILogger<RegistrationController> logger, ILogger<UserRepository> userServiceLogger)
        {
            this._logger = logger;
            this._userService = new UserRepository(userServiceLogger);
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