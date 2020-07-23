using System;
using Microsoft.AspNetCore.Mvc;
using MVCRazorExample.Models;

namespace MVCRazorExample.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        public UserViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            var model = new UserModel()
            {
                IsLoggedIn = true,
                UserName = "user"
            };
            return View("User", model);
        }
    }
}
