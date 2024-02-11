using Microsoft.AspNetCore.Mvc;
using messanger_server.Services;
using messanger_server.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace messanger_server.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private IUserServise _userServise;

        public LoginController()
        {
            this._userServise = new UserService();
        }

        [HttpPost]
        public string Post(string login, string password)
        {
            const string SUCCESS = "successfully logged in!";
            const string FAILED = "failed to log in...";

            if (this._userServise.CheckCorrectLogin(login) &&
                this._userServise.CheckCorrectPassword(password) &&
                this._userServise.LoginAttempt(login, password))
                {
                    return SUCCESS;
                }

            return FAILED;
        }
    }
}
