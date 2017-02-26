using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Fund.Web.Models;
using System.Text;
using Fund.Web.Models;

namespace vipdl.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            DB db = new DB();
            if (db.UserProfiles.Where(p => p.Email == model.Email).Count() != 0)
            {
                ModelState.AddModelError("", "این ایمیل قبلا در سیستم ثبت شده است  !");
                return View(model);
            }
            if (db.UserProfiles.Where(p => p.mobile == model.MobileNumber).Count() != 0)
            {
                ModelState.AddModelError("", "این شماره تلفن قبلا در سیستم ثبت شده است  !");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { @Email = model.Email, @mobile = model.MobileNumber, @password = model.Password });
                    WebSecurity.Login(model.UserName, model.Password);
                    //new Email(model.Email, "ثبت نام شما با موفقیت انجام شد !", new templates().email("register")
                    //.Replace("#email#", model.Email)
                    //.Replace("#username#", model.UserName)
                    //.Replace("#password#", model.Password)
                    //);


                    //new sms(model.MobileNumber, "کاربر گرامی " + model.UserName + " ثبت نام شما با موفقیت انجام شد ! " + Environment.NewLine + "لحظات خوشی را برای شما آرزو مندیم .");
                    //new servers().updateServers();
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));

                }
            }
            return View(model);
        }

        //
        // GET: /Account/Login
        public ActionResult Index()
        {
            return RedirectToAction("index", "home");
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
      
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = HttpUtility.UrlDecode(returnUrl);

            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("", "نام کاربری و رمز عبور را وارد کنید !");
                return View(model);

            }


            if (WebSecurity.Login(model.UserName, model.Password, persistCookie: true))
            {

                var user = new DB().UserProfiles.FirstOrDefault(p => p.UserName == model.UserName);

                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(HttpUtility.UrlDecode(returnUrl));

                return Redirect("~/");

            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه است !");
            return View(model);
        }


        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Manage()
        {
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        DB db = new DB();
                        var user = db.UserProfiles.Find(WebSecurity.CurrentUserId);
                        db.SaveChanges();
                    
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        ModelState.AddModelError("", "انجام شد !");

                    }
                    else
                    {
                        ModelState.AddModelError("", "پسورد فعلی یا پسورد جدید نا معتبر است !");
                    }
                }
            }
         

            // If we got this far, something failed, redisplay form
            return View(model);
        }

  
     
  
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
