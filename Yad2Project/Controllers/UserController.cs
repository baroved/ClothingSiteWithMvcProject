using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAL.Repository;
using Models.Models;
using Yad2Project.ViewModel;
namespace Yad2Project.Controllers
{
    public class UserController : Controller
    {
        public ActionResult LoginPage()
        {
            return PartialView("_LoginPagePartial");
        }

        [HttpPost]
        public ActionResult LoginPage(string username, string password)
        {
            string getStatus = CurrentTime.GetStatus();
            UserRepository repo = new UserRepository();
            HttpCookie cookietime;
            if (repo.LoginUser(username, password))
            {
                if (HttpContext.Request.Cookies["time"] == null)
                {
                    cookietime = new HttpCookie("time");
                    cookietime.Value = getStatus;
                    Response.Cookies.Add(cookietime);
                    cookietime.Expires = DateTime.Now.AddMinutes(1440);
                }
                FormsAuthentication.SetAuthCookie(username, true);
                Response.AddHeader("Refresh", "0.1");
            }
            else
            {
                ModelState.AddModelError("", "username or password are not right");
            }
            return PartialView("_LoginPagePartial");
        }

        public ActionResult CreateUser()
        {
            return PartialView("_CreateUserPartial", new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User newUser)
        {
            UserRepository repo = new UserRepository();
            if (ModelState.IsValid)
            {
                if (repo.CheckAgeOfUser(newUser))
                {
                    if (repo.AddUser(newUser))
                    {
                        FormsAuthentication.SetAuthCookie(newUser.UserName, true);
                        return RedirectToAction("GetProductAviable", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username is already registered");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User must be 18 years old");
                }

            }
            else
            {
                ModelState.AddModelError("", "Model is not valid");
            }
            return PartialView("_CreateUserPartial");
        }

        public ActionResult Edit()
        {
            string name = CookieHelper.GetUserBycookie(GetCookie());
            if (name != null)
            {
                UserRepository userr = new UserRepository();
                User targetuser = userr.GetUser(name);
                return PartialView("_UpdateUserPartial", targetuser);
            }
            else
            {
                return RedirectToAction("CreateUser", "User");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User userForChange)
        {
            UserRepository userr = new UserRepository();
            if (userr.CheckAgeOfUser(userForChange) && ModelState.IsValid && userr.EditUser(userForChange))
            {
                return RedirectToAction("GetProductAviable", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Model is not valid/Age must be at least 18 years old");
                return RedirectToAction("Edit", "User");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GetProductAviable", "Home");
        }

        public HttpCookie GetCookie()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            if (authCookie != null)
                return authCookie;
            return null;
        }

        public void ChangeStatus()
        {
            string getStatus = CurrentTime.GetStatus();
            if (HttpContext.Request.Cookies["time"] != null)
            {
                if (HttpContext.Request.Cookies["time"].Value != getStatus)
                {
                    HttpContext.Request.Cookies["time"].Value = getStatus;
                }
            }

        }
    }
}