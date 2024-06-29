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

        private readonly IUserServise _userServise;

        public RegistrationController(ILogger<RegistrationController> logger)
        {
            this._logger = logger;
            this._userServise = new UserService();
        }

        [HttpPost]
        public string Post(string login, string password, string email)
        {
            const string SUCCESS = "successful registration";
            const string FAILED = "failed registration";

            // Check data is correct
            if (this._userServise.CheckCorrectLogin(login) &&
                this._userServise.CheckCorrectPassword(password) &&
                this._userServise.CheckCorrectEmail(email))
            {
                User newUser = new User()
                {
                    Login = login,
                    Password = password,
                    Email = email
                };

                if (this._userServise.AddUser(newUser))
                {
                    return SUCCESS;
                }

                return FAILED;
            }

            return FAILED;
        }
    }
}