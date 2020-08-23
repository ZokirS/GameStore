using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.WebUI.Infrastrucuture.Abstract;
using GameStore.WebUI.Models;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Не правильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
    }
}