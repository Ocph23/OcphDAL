using AspNetLoginDemo.Models;
using DAL.AspNet.SimpleAuth.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspNetLoginDemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult NotAuth() {
            return View();
        }



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model,string ReturnUrl="")
        {
           if(ModelState.IsValid)
            {
                using (var db = new AuthContext())
                {
                    var result = db.Users.Where(O => O.Email == model.Email).FirstOrDefault();
                    if(result != null)
                    {
                      FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                        if(Url.IsLocalUrl(ReturnUrl))
                            return  Redirect(ReturnUrl);
                        else
                        {
                            return RedirectToAction("Index","Home");
                        }
                    }else
                    {
                        Session.Add("UserName", model.Email);
                        return RedirectToAction("NotAuth");
                    }
                }
            }else 
            {
                ModelState.Remove("Password");
                return View();
            }
        }

        public ActionResult SuccessLogin()
        {
            return View();
        }

    }
}