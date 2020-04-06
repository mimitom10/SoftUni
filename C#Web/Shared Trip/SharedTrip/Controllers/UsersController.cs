using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LogInInputModel input)
        {
            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/Trips/All");
            }

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username must be between 5 and 20 characters");
            }

            if (string.IsNullOrWhiteSpace(input.Email))
            {
                return this.Error("Email cannot be empty!");
            }

            if (input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 characters");
            }

            if (input.Password != input.ConfirmPassword)
            {
                return this.Error("Password should match.");
            }

            if (this.usersService.EmailExists(input.Email))
            {
                return this.Error("Email already in use.");
            }

            if (this.usersService.UsernameExists(input.Username))
            {
                return this.Error("Username already in use.");
            }

            this.usersService.Register(input.Username, input.Email, input.Password);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
