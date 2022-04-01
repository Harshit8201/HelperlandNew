using Helperland.Data;
using Helperland.Models;
using Helperland.Models.ViewModel;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HelperlandContext _context;
        public CustomerController(HelperlandContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CustomerPages()
        {
            int userID = Convert.ToInt32(HttpContext.Session.GetInt32("userID"));
            IEnumerable<CustomerServiceHistoryViewModel> serviceRequests = GetCustomerServiceHistory(userID);
            return View(serviceRequests);
        }

        public List<CustomerServiceHistoryViewModel> GetCustomerServiceHistory(int UserID)
        {
            try
            {
                List<ServiceRequest> objServiceRequest = _context.ServiceRequests.Where(x => x.UserId == UserID && (x.Status == null )).ToList();
                List<CustomerServiceHistoryViewModel> objCustomerHistory = new List<CustomerServiceHistoryViewModel>();
                foreach (var item in objServiceRequest)
                {
                    CustomerServiceHistoryViewModel objTempCustomerHistory = new CustomerServiceHistoryViewModel();
                    objTempCustomerHistory.UserId = UserID;
                    objTempCustomerHistory.ServiceId = item.ServiceId;
                    objTempCustomerHistory.ServiceRequestId = item.ServiceRequestId;
                    objTempCustomerHistory.ServiceProviderId = item.ServiceProviderId;
                    if (objTempCustomerHistory.ServiceProviderId != null)
                    {
                        User objUser = _context.Users.Where(x => x.UserId == objTempCustomerHistory.ServiceProviderId).FirstOrDefault();
                        objTempCustomerHistory.ServiceProviderName = objUser.FirstName + " " + objUser.LastName;
                    }
                    objTempCustomerHistory.ServiceStartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                    objTempCustomerHistory.TotalCost = item.TotalCost;
                    objCustomerHistory.Add(objTempCustomerHistory);

                }
                return objCustomerHistory;
            }
            catch (Exception ex)
            {
                string _Message = ex.Message;
                return null;
            }

        }
        public IActionResult ServiceHistory()
        {
            int userId = (int)HttpContext.Session.GetInt32("userID");
            return View(GetServicesHistoryByUserId(userId));
        }

        public List<ServiceHistoryViewModel> GetServicesHistoryByUserId(int userID)
        {
            List<ServiceRequest> sr = _context.ServiceRequests.Where(x => x.UserId == userID && x.Status != null).ToList();
            List<ServiceHistoryViewModel> csv = new List<ServiceHistoryViewModel>();
            foreach (var item in sr)
            {
                ServiceHistoryViewModel l = new ServiceHistoryViewModel();
                l.ServiceId = item.ServiceRequestId;
                l.ServiceProvideId = item.ServiceProviderId;
                if (l.ServiceProvideId != null)
                {
                    User u = _context.Users.Where(x => x.UserId == l.ServiceProvideId).FirstOrDefault();
                    l.ServiceProviderName = u.FirstName + " " + u.LastName;
                }
                l.StartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                l.Status = item.Status;
                if (l.Status == 2)
                {
                    l.Rate = false;

                }
                else
                {
                    Rating rating = _context.Ratings.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                    if (rating == null)
                        l.Rate = true;
                    else
                        l.Rate = false;
                }

                l.Rating = GetRating(item.ServiceProviderId);
                csv.Add(l);
            }
            return csv;
        }

        public decimal? GetRating(int? serviceProviderId)
        {
            if (serviceProviderId == null)
            {
                return null;
            }
            else
            {
                List<Rating> ratings = _context.Ratings.Where(x => x.RatingTo == serviceProviderId).ToList();
                int i = 0;
                decimal sum = 0;
                foreach (var item in ratings)
                {
                    sum += item.Ratings;
                    i++;
                }
                if (i == 0)
                {
                    return 0;
                }
                else
                {
                    return (sum / i);
                }

            }
        }



        [HttpGet]
        public IActionResult MySettings()
        {
            return View();
        }


        public bool UpdateServiceDate(string serviceDate, string serviceId)
        {
            bool serviceUpdated = UpdateServiceDates(Int32.Parse(serviceId), Convert.ToDateTime(serviceDate));
            return serviceUpdated;
        }

        public bool UpdateServiceDates(int serviceId, DateTime serviceDate)
        {
            ServiceRequest request = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceId).FirstOrDefault();

            if (request.ServiceProviderId == null)
            {
                request.ServiceStartDate = serviceDate;
                _context.ServiceRequests.Update(request);
                _context.SaveChanges();
                return true;
            }
            else
            {
                ServiceRequest objServiceRequest = _context.ServiceRequests.Where(x => x.ServiceProviderId == request.ServiceProviderId && x.ServiceStartDate == serviceDate).FirstOrDefault();
                if (objServiceRequest == null)
                {

                    User user = _context.Users.Where(x => x.UserId == request.ServiceProviderId).FirstOrDefault();
                    //string welcomeMessage = "Welcome to Helperland,   <br/> Service Date change for Service ID " + request.ServiceId + " <br/> <b> Check it now <b>";
                    //request.ServiceStartDate = serviceDate;

                    //String To = user.Email;
                    //String subject = "Service Request Date Change ";
                    //String Body = "Date Change" + " : " + welcomeMessage;
                    //MailMessage obj = new MailMessage();
                    //obj.To.Add(To);
                    //obj.Subject = subject;
                    //obj.Body = Body;
                    //obj.From = new MailAddress("--");
                    //obj.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    //smtp.Port = 587;
                    //smtp.UseDefaultCredentials = true;
                    //smtp.EnableSsl = true;
                    //smtp.Credentials = new System.Net.NetworkCredential("--", "");
                    //smtp.Send(obj);
                    _context.ServiceRequests.Update(request);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool CancelService(string serviceId, string message)
        {
            if (serviceId != null && message != null)
            {
                bool serviceCancel = CancelServices(Int32.Parse(serviceId), message);
                return serviceCancel;
            }
            else
            {
                return false;
            }

        }

        public bool CancelServices(int serviceId, string message)
        {
            ServiceRequest objServiceRequest = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceId).FirstOrDefault();
            objServiceRequest.Status = 2;
            
                _context.ServiceRequests.Update(objServiceRequest);
                _context.SaveChanges();
                //if (objServiceRequest.ServiceProviderId != null)
                //{
                    
                //    string welcomeMessage = "Welcome to Helperland,   <br/> Service Request no  " + objServiceRequest.ServiceRequestId + " is canceled due to below reason. <br/>" + message;
                //    User objUser = _context.Users.Where(x => x.UserId == objServiceRequest.ServiceProviderId).FirstOrDefault();

                //    String To = objUser.Email;
                //    String subject = "Service Cancel Reason ";
                //    String Body = "Cancel Service because of " + " : " + welcomeMessage;
                //    MailMessage obj = new MailMessage();
                //    obj.To.Add(To);
                //    obj.Subject = subject;
                //    obj.Body = Body;
                //    obj.From = new MailAddress("dreamers96845@gmail.com");
                //    obj.IsBodyHtml = true;
                //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //    smtp.Port = 587;
                //    smtp.UseDefaultCredentials = true;
                //    smtp.EnableSsl = true;
                //    smtp.Credentials = new System.Net.NetworkCredential("dreamers96845@gmail.com", "goals@2022");
                //    smtp.Send(obj);

                //}

                return true;
        }

        public bool ResetPassword(int userID,string oldPassword, string newPassword)
        {
            return ResetPasswords(oldPassword, newPassword);
        }

        [HttpPost]
        public IActionResult UpdateUserData(string FirstName, string LastName, string DOB, string MobileNumber, string UserId, string LanguageId)
        {
            
            var i = ChangeUserData(FirstName, LastName, DOB, MobileNumber, Int32.Parse(LanguageId));
            return Json(i);
        }

        public Boolean ChangeUserData(string FirstName, string LastName, string DOB, string MobileNumber, int LanguageId)
        {
            int UserID = (int)HttpContext.Session.GetInt32("userID");
            User user = _context.Users.Where(x => x.UserId == UserID).FirstOrDefault();
            user.FirstName = FirstName;
            user.LanguageId = LanguageId;
            user.LastName = LastName;
            user.DateOfBirth = Convert.ToDateTime(DOB);
            user.Mobile = MobileNumber;
                _context.Users.Update(user);
                _context.SaveChanges();
                HttpContext.Session.SetString("FirstName", user.FirstName);
                return true;
            
            
        }

        


        public bool ResetPasswords(string OldPassword, string NewPassword)
        {
            int userID = (int)HttpContext.Session.GetInt32("userID");
            try
            {
                User user = _context.Users.Find(userID);
                string pass = user.Password;
                if (pass == OldPassword)
                {
                    user.Password = NewPassword;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool AddRating(string ServiceReqestId, string onTime, string friendly, string qualityOfService, string feedBack)
        {
            return AddRatings(Int32.Parse(ServiceReqestId), decimal.Parse(onTime), decimal.Parse(friendly), decimal.Parse(qualityOfService), feedBack);
        }



        public bool AddRatings(int serviceRequestId, decimal onTime, decimal friendly, decimal qualityOfService, string feedBack)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            Rating rating = new Rating();
            
            
            rating.ServiceRequestId = sr.ServiceRequestId;
            rating.RatingFrom = sr.UserId;
            
            rating.RatingTo = (int)sr.ServiceProviderId;
            rating.Comments = feedBack;

            rating.OnTimeArrival = onTime;
            rating.Friendly = friendly;
            rating.QualityOfService = qualityOfService;
            decimal i = (friendly + onTime + qualityOfService) / 3;
            rating.Ratings = i;
            rating.RatingDate = DateTime.Now;
            try
            {
                _context.Ratings.Add(rating);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public IActionResult GetServiceRequestData(string ServiceRequestId)
        {

            return Json(JsonConvert.SerializeObject(GetServiceDetails(Int32.Parse(ServiceRequestId))));
        }


        public object GetServiceDetails(int serviceRequestId)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            List<ServiceRequestExtra> sre = _context.ServiceRequestExtras.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
            ServiceRequestAddress sra = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            string name = "";
            User u = _context.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
            if (sr.ServiceProviderId != null)
            {
                User user = _context.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                name = user.FirstName + " " + user.LastName;
            }

            string extra = "";
            foreach (var i in sre)
            {
                if (i.ServiceExtraId == 1)
                    extra += "Inside Cabinet, ";
                if (i.ServiceExtraId == 2)
                    extra += "Inside Fridge, ";
                if (i.ServiceExtraId == 3)
                    extra += "Interior Oven, ";
                if (i.ServiceExtraId == 4)
                    extra += "Interior Window, ";
                if (i.ServiceExtraId == 5)
                    extra += "Laundry & Wash, ";
            }
            var temp = new
            {
                ServiceDate = sr.ServiceStartDate.ToString("dd/MM/yyyy"),
                TotalHour = sr.ServiceHours,
                Extra = extra,
                TotalCost = sr.TotalCost,
                Address = sra.AddressLine1 + " , " + sra.AddressLine2 + " , " + sra.PostalCode,
                MobileNumber = sra.Mobile,
                Email = u.Email,
                ServiceProviderId = sr.ServiceProviderId,
                Comments = sr.Comments,
                Haspet = sr.HasPets,
                ServiceProviderName = name,
                Rating = GetRating(sr.ServiceProviderId)
            };
            return temp;

        }

        public IActionResult yourDetail()
        {
            var ty = (int)HttpContext.Session.GetInt32("userID");
            List<UserAddress> u = _context.UserAddresses.Where(x => x.UserId == ty).ToList();
            System.Threading.Thread.Sleep(2000);
            return View(u);
        }


        [HttpPost]
        public string yourDetail([FromBody] UserAddress address)
        {
            address.UserId = (int)HttpContext.Session.GetInt32("userID");
            _context.UserAddresses.Add(address);
            _context.SaveChanges();
            return "true";
        }



        public string detailsTab([FromBody] User test)
        {
            var ty = (int)HttpContext.Session.GetInt32("userID");
            User u = _context.Users.Where(x => x.UserId == ty).FirstOrDefault();
            u.FirstName = "harsh";
            u.LastName = "rajani";
            u.Mobile = "8758515117";
            _context.Users.Update(u);
            _context.SaveChanges();
            return "true";
        }



        public IActionResult addressMenu()
        {
            var ty = (int)HttpContext.Session.GetInt32("userID");
            List<UserAddress> tu = _context.UserAddresses.Where(x => x.UserId == ty).ToList();
            return View(tu);
        }

        public string deleteTab(int i)
        {
            UserAddress u = _context.UserAddresses.Where(x => x.AddressId == i).FirstOrDefault();
            _context.UserAddresses.Remove(u);
            _context.SaveChanges();
            return "true";
        }

        public IActionResult editAddress(int edit)
        {
            UserAddress u = _context.UserAddresses.Where(x => x.AddressId == edit).FirstOrDefault();
            return View(u);
        }

        public string editPopup([FromBody] UserAddress change)
        {
            UserAddress getaddress = _context.UserAddresses.Where(x => x.AddressId == change.AddressId).FirstOrDefault();

            getaddress.AddressLine1 = change.AddressLine1;
            getaddress.AddressLine2 = change.AddressLine2;
            getaddress.PostalCode = change.PostalCode;
            getaddress.City = change.City;
            getaddress.Mobile = change.Mobile;
            _context.UserAddresses.Update(getaddress);
            _context.SaveChanges();
            return "true";

        }

    }

}
