using bartnikwolski.Models;
using bartnikwolski.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace bartnikwolski.Controllers
{
    public class UsersController : Controller
    {
        BeekeeperDbContext db = new BeekeeperDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Login == model.Login) && Crypto.VerifyHashedPassword(db.Users.First(u => u.Login == model.Login).Password, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ModelState.AddModelError("", "Login bądź hasło niepoprawne.");
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            return RedirectToAction("Index", "Home");
        }
    }
}