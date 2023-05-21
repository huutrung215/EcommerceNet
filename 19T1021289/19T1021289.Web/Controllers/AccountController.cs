using _19T1021289.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021289.DomainModels;
using System.Web.Security;

namespace _19T1021289.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName="", string password="")
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }


            ViewBag.UserName = userName;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại!");
                return View();
            }
            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword(string userName, string oldPassword = "", string newPassword="", string confirmPassword="") {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }

            ViewBag.UserName = userName;
            ViewBag.OldPassword = oldPassword;
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, oldPassword);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "mat khau sai!");
                return View();
            }
            UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword, confirmPassword);
            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}