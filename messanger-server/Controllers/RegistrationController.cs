using Microsoft.AspNetCore.Mvc;
using messanger_server.Services;
using messanger_server.Services.Interfaces;

// Add documentation
namespace messanger_server.Controllers
{
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;

        private IUserServise _userServise;

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
                this._userServise.AddUser(new Models.User() { Login = login, Password = password, Email = email});
                return SUCCESS;
            }

            return FAILED;
        }
    }
}