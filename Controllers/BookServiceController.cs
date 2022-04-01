using Helperland.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Helperland.Data;
using Microsoft.AspNetCore.Http;
using Helperland.Models.ViewModel;
using System.Net.Mail;

namespace Helperland.Controllers
{

    public class BookServiceController : Controller
    {

        private readonly HelperlandContext _context;

        public BookServiceController(HelperlandContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult BookService()
        {
            return View();
        }
        [HttpPost]
        public JsonResult CheckPincode(string zipcode)
             {

            if (zipcode == null || zipcode.Length > 7 || !Regex.IsMatch(zipcode, @"^[0-9]{6}$"))
            {
                return Json(false);
            }
            else
            {
                if (IsServiceAvailable(zipcode))
                {
                    return Json(true);
                }
                else
                    return Json("We are not providing service in this area. We’ll notify you if any helper would start working near your area.");
            }
        }
        [HttpPost]
        public bool IsServiceAvailable(string strZipcode)
        {
            if (strZipcode == null)
            {
                return false;
            }
            else
            {
                User verify = _context.Users.FirstOrDefault(x => x.ZipCode.Equals(strZipcode));
                try
                {
                    if (verify != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    return false;
                }

            }
        }

        [HttpPost]
        public IActionResult AddAddress(UserAddress userAddressModel)
        {
            if (userAddressModel.UserId.ToString() != null && userAddressModel.AddressLine1 != null && userAddressModel.AddressLine2 != null && userAddressModel.City != null)
            {
                if (SetAddress(userAddressModel))
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }

        }

        public bool SetAddress(UserAddress userAddressModel)
        {
            

            UserAddress userAddress = new UserAddress();
            userAddress.AddressLine1 = userAddressModel.AddressLine1;
            userAddress.AddressLine2 = userAddressModel.AddressLine2;
            userAddress.City = userAddressModel.City;
            userAddress.State = userAddressModel.State;
            userAddress.PostalCode = userAddressModel.PostalCode;
            userAddress.UserId = (int)userAddressModel.UserId;
            userAddress.Mobile = userAddressModel.Mobile;
            try
            {
                _context.UserAddresses.Add(userAddress);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                message += ex.InnerException.Message;
                return false;
            }
        }


        public IActionResult GetAddress(string userID)
        {
            List<UserAddress> addresses = GetAddresses(Int32.Parse(userID));
            if (addresses == null)
            {
                Json(false);
            }
            return Json(JsonConvert.SerializeObject(addresses));

        }

        public List<UserAddress> GetAddresses(int userID)
        {
            try
            {
                List<UserAddress> addresses = new List<UserAddress>();
                List<UserAddress> userAddresses = _context.UserAddresses.Where(x => x.UserId == userID).ToList();
                foreach (var item in userAddresses)
                {
                    UserAddress address = new UserAddress();
                    address.UserId = userID;
                    address.AddressId = item.AddressId;
                    address.AddressLine1 = item.AddressLine1;
                    address.AddressLine2 = item.AddressLine2;
                    address.Mobile = item.Mobile;
                    address.City = item.City;
                    addresses.Add(address);

                }
                return addresses;
            }
            catch (Exception ex)
            {
                string _Message = ex.Message.ToString();
                return null;
            }
        }

        [HttpPost]
        public int AddService(BookServiceViewModel bookServiceViewModel)
        {
            
                ServiceRequest serviceRequest = new ServiceRequest();
                serviceRequest.UserId = bookServiceViewModel.UserId;
                
                serviceRequest.ServiceStartDate = bookServiceViewModel.ServiceStartDate;
                serviceRequest.ZipCode = bookServiceViewModel.ZipCode;
                serviceRequest.Comments = bookServiceViewModel.Comments;
                serviceRequest.TotalCost = bookServiceViewModel.TotalCost;
                serviceRequest.SubTotal = bookServiceViewModel.SubTotal;
                serviceRequest.HasPets = bookServiceViewModel.HasPets;
                serviceRequest.PaymentDone = true;
                serviceRequest.ServiceId = 1;
                serviceRequest.CreatedDate = DateTime.Now;
                serviceRequest.ServiceHourlyRate = 18;
                _context.ServiceRequests.Add(serviceRequest);
                _context.SaveChanges();

                UserAddress userAddress = _context.UserAddresses.Where(x => x.AddressId == bookServiceViewModel.AddressId).FirstOrDefault();
                ServiceRequestAddress serviceRequestAddress = new ServiceRequestAddress();
                serviceRequestAddress.ServiceRequestId = serviceRequest.ServiceRequestId;
                serviceRequestAddress.AddressLine1 = userAddress.AddressLine1;
                serviceRequestAddress.AddressLine2 = userAddress.AddressLine2;
                serviceRequestAddress.City = userAddress.City;
                serviceRequestAddress.State = userAddress.State;
                serviceRequestAddress.PostalCode = userAddress.PostalCode;
                serviceRequestAddress.Mobile = userAddress.Mobile;
                serviceRequestAddress.Email = userAddress.Email;
                _context.ServiceRequestAddresses.Add(serviceRequestAddress);
                _context.SaveChanges();

                if (bookServiceViewModel.InsideCabinet == true)
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = serviceRequest.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 1;
                    _context.ServiceRequestExtras.Add(serviceRequestExtra);
                    _context.SaveChanges();

                }
                if (bookServiceViewModel.InsideFridge == true)
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = serviceRequest.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 2;
                    _context.ServiceRequestExtras.Add(serviceRequestExtra);
                    _context.SaveChanges();
                }
                if (bookServiceViewModel.InteriorOven == true)
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = serviceRequest.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 3;
                    _context.ServiceRequestExtras.Add(serviceRequestExtra);
                    _context.SaveChanges();
                }
                if (bookServiceViewModel.InteriorWindows == true)
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = serviceRequest.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 4;
                    _context.ServiceRequestExtras.Add(serviceRequestExtra);
                    _context.SaveChanges();
                }
                if (bookServiceViewModel.LaundryWashDry == true)
                {
                    ServiceRequestExtra serviceRequestExtra = new ServiceRequestExtra();
                    serviceRequestExtra.ServiceRequestId = serviceRequest.ServiceRequestId;
                    serviceRequestExtra.ServiceExtraId = 5;
                    _context.ServiceRequestExtras.Add(serviceRequestExtra);
                    _context.SaveChanges();
                }
            List<User> users = new List<User>();
            if (bookServiceViewModel.HasPets)
            {
                users = _context.Users.Where(x => x.ZipCode == bookServiceViewModel.ZipCode && x.WorksWithPets == bookServiceViewModel.HasPets && x.UserTypeId == 1).ToList();
            }
            else
            {
                users = _context.Users.Where(x => x.ZipCode == bookServiceViewModel.ZipCode && x.UserTypeId == 1).ToList();
            }

            foreach (var item in users)
            {
                string welcomeMessage = "Welcome to Helperland,   <br/> You get new Service request. <br/> <b> Check it now <b>";

                String To = item.Email;
                String subject = "New Service Request";
                String Body = "You got new Service request " + " : " + welcomeMessage;
                MailMessage obj = new MailMessage();
                obj.To.Add(To);
                obj.Subject = subject;
                obj.Body = Body;
                obj.From = new MailAddress("dummy");
                obj.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("--", "--");
                smtp.Send(obj);
            }
            return serviceRequest.ServiceRequestId;    
        }
        [HttpPost]
        public JsonResult Details([FromBody] ServiceRequestAddress address)
        {
            _context.ServiceRequestAddresses.Add(address);
            _context.SaveChanges();
            return Json(true);
        }
    }
}

