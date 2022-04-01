using Helperland.Data;
using Helperland.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HelperlandContext _auc;

        public AccountController(ILogger<HomeController> logger, HelperlandContext auc)
        {
            _logger = logger;
            _auc = auc;
        }
        public IActionResult Index(bool isLogged = false, bool isLoginOpen = false)
        {
            ViewBag.IsLogged = isLogged;
            ViewBag.isLoginOpen = isLoginOpen;
            ViewBag.isOpen = false;
            ViewBag.isForgetPasswordOpen = false;
            ViewBag.Success = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateForCustomer(User uc)
        {
            var verify = _auc.Users.FirstOrDefault(x => x.Email.Equals(uc.Email));
            if (verify == null)
            {
                uc.UserTypeId = 0;
                uc.ZipCode = "123456";
                _auc.Add(uc);
                _auc.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                string errormsg = "Email is already registered...login instead !";
                TempData["ErrorMessage"] = errormsg;
                return RedirectToAction("NewCustomerAccount", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateForProvider(User uc)
        {
            var verify = _auc.Users.FirstOrDefault(x => x.Email.Equals(uc.Email));
            if (verify == null)
            {
                uc.UserTypeId = 1;
                uc.ZipCode = "234567";
                _auc.Add(uc);
                _auc.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string errormsg = "Email is already registered...login instead !";
                TempData["ErrorMessage"] = errormsg ;
                return RedirectToAction("BecomeHelper", "Home");
            }
        }


        [HttpPost]
        public IActionResult Login(User obj)
            {
                User user = _auc.Users.Where(x => x.Email == obj.Email && x.Password == obj.Password).SingleOrDefault();
                if (user != null)
                {
                HttpContext.Session.SetInt32("User_Session", user.UserId);
                HttpContext.Session.SetInt32("userID", user.UserId);
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetInt32("usertype", user.UserTypeId);
                int userType = user.UserTypeId;
                if (userType == 2)
                {
                    return RedirectToAction("Index", "AdminPortion");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                
                }
                else
                {
                string errormsg = "Invalid Email or password !";
                TempData["ErrorforLogin"] = errormsg;
                return RedirectToAction("Index","home");
                }
            }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User_Session");
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPasswordResetlink(User uc)
        {
            string baseUrl = string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host);
            var activationUrl = $"{baseUrl}/Account/NewPassword";
            var verify = (from x in _auc.Users where x.Email == uc.Email select x).FirstOrDefault();
            
                String To = uc.Email;
                String subject = "Helperland - Reset your password ";
                String Body = "Reset password" + " : " + activationUrl;
                MailMessage obj = new MailMessage();
                obj.To.Add(To);
                obj.Subject = subject;
                obj.Body = Body;
                obj.From = new MailAddress("harshitrajani1988@gmail.com");
                obj.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("harshitrajani1988@gmail.com", "LdrptoTatvasoft");
                smtp.Send(obj);
                ViewBag.message = "The email has been sent";
                return RedirectToAction("Index","Home");
          

        }
        [HttpGet]
        public IActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewPassword(User uc)
        {
            var verify = _auc.Users.FirstOrDefault(x => x.Email.Equals(uc.Email));
            if (verify != null)
            {
                verify.Email = uc.Email;
                verify.Password = uc.Password;
                verify.Confirmpassword = uc.Confirmpassword;
                _auc.Users.Update(verify);
                _auc.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email";
                return RedirectToAction("NewPassword"); 
            }
        }

    }
}
