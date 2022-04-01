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
    public class ServiceProviderController : Controller
    {
        private readonly HelperlandContext _context;

        public ServiceProviderController(HelperlandContext context)
        {
            _context = context;
        }
        public IActionResult SPSettings()
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            SPDetailsViewModel model = new SPDetailsViewModel();
            User user;
            user = GetUserById(ServiceProviderUserID);
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.lastName = user.LastName;
                model.month = Convert.ToDateTime(user.DateOfBirth).Month;
                model.day = Convert.ToDateTime(user.DateOfBirth).Day;
                model.year = Convert.ToDateTime(user.DateOfBirth).Year;
                model.mobileNumber = user.Mobile;
                model.email = user.Email;
                if (user.Gender != null)
                    model.Gender = (int)user.Gender;
                if (user.NationalityId != null)
                    model.NationalityId = (int)user.NationalityId;

                model.UserProfilePicture = user.UserProfilePicture;
                if (user.IsActive != false)
                    model.IsActive = (bool)user.IsActive;
                else
                    model.IsActive = false;

            }
            return View(model);
        }

        public User GetUserById(int userID)
        {
            User user = new User();
            try
            {
                user = _context.Users.Where(x => x.UserId == userID).FirstOrDefault();
                if (user != null)
                    return user;
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpPost]
        public IActionResult GetUserData(SPDetailsViewModel sPDetailsViewModel)
        {

            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            UpdateServiceProviderData(sPDetailsViewModel, ServiceProviderUserID);
            return RedirectToAction("SPSettings");
        }


        public void UpdateServiceProviderData(SPDetailsViewModel sPDetailsViewModel, int userId)
        {
            User user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            UserAddress userAddress = _context.UserAddresses.Where(x => x.UserId == userId).FirstOrDefault();
            user.FirstName = sPDetailsViewModel.FirstName;
            user.LastName = sPDetailsViewModel.lastName;
            user.Mobile = sPDetailsViewModel.mobileNumber;
            user.DateOfBirth = Convert.ToDateTime(sPDetailsViewModel.day + "/" + sPDetailsViewModel.month + "/" + sPDetailsViewModel.year);
            user.NationalityId = sPDetailsViewModel.NationalityId;
            user.Gender = sPDetailsViewModel.Gender;
            user.UserProfilePicture = sPDetailsViewModel.UserProfilePicture;
            user.ZipCode = sPDetailsViewModel.Zipcode;
            bool isNull = false;
            if (userAddress == null)
            {
                userAddress = new UserAddress();
                userAddress.UserId = userId;
                isNull = true;

            }
            userAddress.AddressLine1 = sPDetailsViewModel.AddressLine1;
            userAddress.AddressLine2 = sPDetailsViewModel.AddressLine2;
            userAddress.PostalCode = sPDetailsViewModel.Zipcode;

            _context.Users.Update(user);
            if (isNull)
                _context.UserAddresses.Add(userAddress);
            else
                _context.UserAddresses.Update(userAddress);
            _context.SaveChanges();

        }

        public bool ResetPassword(string oldPassword, string newPassword)
        {
            return ResetPasswords(oldPassword, newPassword);
        }

        public bool ResetPasswords( string OldPassword, string NewPassword)
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

        [HttpGet]
        public IActionResult NewServiceRequest(string hasPate)
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            bool hasPasts;
            if (hasPate == "on")
            {
                hasPasts = true;
            }
            else
            {
                hasPasts = false;
            }
            ViewBag.hasPat = hasPasts;
            return View(GetServiceRequestsNotAccepted(ServiceProviderUserID, hasPasts));
        }

        public List<NewServiceRequestViewModel> GetServiceRequestsNotAccepted(int userId, bool hasPate)
        {

            User u = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            List<ServiceRequest> sr = _context.ServiceRequests.Where(x => x.Status == null && x.ServiceProviderId == null /*&& x.ZipCode == u.ZipCode*/ && x.HasPets == hasPate).ToList();
            List<NewServiceRequestViewModel> newServiceRequestViewModels = new List<NewServiceRequestViewModel>();

            foreach (var item in sr)
            {
                FavoriteAndBlocked obj = _context.FavoriteAndBlockeds.Where(x => x.UserId == userId && x.TargetUserId == item.UserId && x.IsBlocked == true).FirstOrDefault();
                bool IsBlocked;
                if (obj != null)
                {
                    IsBlocked = true;
                }
                else
                {
                    IsBlocked = false;
                }
                if (IsBlocked == false)
                {
                    NewServiceRequestViewModel l = new NewServiceRequestViewModel();
                    l.ServiceRequestID = item.ServiceRequestId;
                    l.ServicestartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                    l.Payment = item.TotalCost;
                    User user = _context.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                    l.CustomerName = user.FirstName + " " + user.LastName;
                    ServiceRequestAddress adress = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                    l.Addressline1 = adress.AddressLine1 + " " + adress.AddressLine2;
                    l.Addressline2 = adress.PostalCode + " " + adress.City;
                    newServiceRequestViewModels.Add(l);
                }
                

            }
            newServiceRequestViewModels.Reverse();
            return newServiceRequestViewModels;

        }


        public IActionResult MyRatings()
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return View(GetRatingsForServiceProvider(ServiceProviderUserID));
        }


        public List<RatingsViewModel> GetRatingsForServiceProvider(int serviceproviderId)
        {
            List<RatingsViewModel> ratingsViewModels = new List<RatingsViewModel>();
            List<Rating> ratings = _context.Ratings.Where(x => x.RatingTo == serviceproviderId).ToList();
            if (ratings != null)
            {
                foreach (var item in ratings)
                {
                    RatingsViewModel r = new RatingsViewModel();
                    User u = _context.Users.Where(x => x.UserId == item.RatingFrom).FirstOrDefault();
                    ServiceRequest s = _context.ServiceRequests.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                    r.RatingID = item.RatingId;
                    r.CustomerName = u.FirstName + " " + u.LastName;
                    r.Comments = item.Comments;
                    r.Rating = item.Ratings;
                    r.ServiceRequestID = item.ServiceRequestId;
                    r.ServiceDate = s.ServiceStartDate.ToString("dd/MM/yyyy");
                    ratingsViewModels.Add(r);
                }
                return ratingsViewModels;
            }
            else
                return null;

        }
        public IActionResult BlockUser()
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return View(GetListOfCustomer(ServiceProviderUserID));
        }


        public List<BlockCustomerViewModel> GetListOfCustomer(int userId)
        {
            List<BlockCustomerViewModel> lists = new List<BlockCustomerViewModel>();
            List<ServiceRequest> li = _context.ServiceRequests.Where(x => x.ServiceProviderId == userId).ToList();
            foreach (var item in li)
            {
                try
                {
                    User user = _context.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                    BlockCustomerViewModel temp = new BlockCustomerViewModel();
                    FavoriteAndBlocked favoriteAndBlocked = _context.FavoriteAndBlockeds.Where(x => x.UserId == item.ServiceProviderId && x.TargetUserId == item.UserId).FirstOrDefault();
                    if (favoriteAndBlocked == null)
                    {
                        temp.UserId = user.UserId;
                        temp.Username = user.FirstName + " " + user.LastName;
                        temp.IsBlock = false;
                    }
                    else
                    {
                        temp.UserId = user.UserId;
                        temp.Username = user.FirstName + " " + user.LastName;
                        temp.IsBlock = favoriteAndBlocked.IsBlocked;
                    }
                    bool alreadyExists = lists.Any(x => x.UserId == temp.UserId);
                    if (!alreadyExists)
                    {
                        lists.Add(temp);
                    }
                }
                catch (Exception ex)
                {
                    return lists;
                }
            }
            return lists;
        }

        public bool SetBlockCustomer(string userId)
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return BlockCustomer(ServiceProviderUserID, Int32.Parse(userId));
        }

        public bool BlockCustomer(int userId, int targetUserId)
        {
            FavoriteAndBlocked favoriteAndBlocked = _context.FavoriteAndBlockeds.Where(x => x.UserId == userId && x.TargetUserId == targetUserId).FirstOrDefault();
            if (favoriteAndBlocked == null)
            {
                favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.UserId = userId;
                favoriteAndBlocked.TargetUserId = targetUserId;
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = true;
                _context.FavoriteAndBlockeds.Add(favoriteAndBlocked);
                _context.SaveChanges();
            }
            else
            {
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = true;
                _context.FavoriteAndBlockeds.Update(favoriteAndBlocked);
                _context.SaveChanges();
            }
            return true;
        }



        public bool SetUnblockCustomer(string userId)
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return UnblockCustomer(ServiceProviderUserID, Int32.Parse(userId));
        }


        public bool UnblockCustomer(int userId, int targetUserId)
        {
            FavoriteAndBlocked favoriteAndBlocked = _context.FavoriteAndBlockeds.Where(x => x.UserId == userId && x.TargetUserId == targetUserId).FirstOrDefault();
            if (favoriteAndBlocked == null)
            {
                favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.UserId = userId;
                favoriteAndBlocked.TargetUserId = targetUserId;
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = false;
                _context.FavoriteAndBlockeds.Add(favoriteAndBlocked);
                _context.SaveChanges();
            }
            else
            {
                favoriteAndBlocked.IsFavorite = false;
                favoriteAndBlocked.IsBlocked = false;
                _context.FavoriteAndBlockeds.Update(favoriteAndBlocked);
                _context.SaveChanges();
            }
            return true;
        }


        public IActionResult SpServiceHistory()
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return View(GetServiceHistoryForServiceProvider(ServiceProviderUserID));
        }

        public List<SpServiceHistoryViewModel> GetServiceHistoryForServiceProvider(int userId)
        {
            List<ServiceRequest> sr = _context.ServiceRequests.Where(x => x.ServiceProviderId == userId && x.Status == 1).ToList();
            List<SpServiceHistoryViewModel> li = new List<SpServiceHistoryViewModel>();
            foreach (var item in sr)
            {
                User u = _context.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                ServiceRequestAddress sra = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                SpServiceHistoryViewModel temp = new SpServiceHistoryViewModel();
                temp.ServiceRequestId = item.ServiceRequestId;
                temp.ServiceDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                temp.CustomerName = u.FirstName + " " + u.LastName;
                temp.AddressLine1 = sra.AddressLine1 + " ," + sra.AddressLine2;
                temp.AddressLine2 = sra.PostalCode + " , " + sra.City;
                li.Add(temp);

            }
            return li;
        }

        public IActionResult GetServiceRequestData(string ServiceReqestId)
        {
            return Json(JsonConvert.SerializeObject(GetServiceDetailsforServiceProvider(Int32.Parse(ServiceReqestId))));
        }


        public object GetServiceDetailsforServiceProvider(int serviceRequestId)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            List<ServiceRequestExtra> sre = _context.ServiceRequestExtras.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
            ServiceRequestAddress sra = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            User u = _context.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
       
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
                Address = sra.AddressLine1 + " , " + sra.AddressLine2 + " , " + sra.PostalCode + " , " + sra.City,
                ServiceProviderId = sr.ServiceProviderId,
                Comments = sr.Comments,
                Haspet = sr.HasPets,
                CustomerName = u.FirstName + " " + u.LastName
            };
            return temp;

        }


        public IActionResult UpComingService()
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return View(GetServiceRequestsIsAccepted(ServiceProviderUserID));
        }


        public List<NewServiceRequestViewModel> GetServiceRequestsIsAccepted(int userId)
        {
            User u = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            List<ServiceRequest> sr = _context.ServiceRequests.Where(x => x.Status == 3 && x.ServiceProviderId == userId).ToList();
            List<NewServiceRequestViewModel> newServiceRequestViewModels = new List<NewServiceRequestViewModel>();
            foreach (var item in sr)
            {
                NewServiceRequestViewModel l = new NewServiceRequestViewModel();
                l.ServiceRequestID = item.ServiceRequestId;
                l.ServicestartDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                l.Payment = item.TotalCost;
                User user = _context.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                l.CustomerName = user.FirstName + " " + user.LastName;
                ServiceRequestAddress adress = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == item.ServiceRequestId).FirstOrDefault();
                l.Addressline1 = adress.AddressLine1 + " " + adress.AddressLine2;                
                l.Addressline2 = adress.PostalCode + " " + adress.City;
                newServiceRequestViewModels.Add(l);


            }
            newServiceRequestViewModels.Reverse();
            return newServiceRequestViewModels;

        }


        public bool CanceledServiceRequest(string serviceRequeestId, string message)
        {
            return CancelServiceRequest(Int32.Parse(serviceRequeestId), message);
        }

        public bool CancelServiceRequest(int serviceRequeestId, string message)
        {
            ServiceRequest objServiceRequest = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequeestId).FirstOrDefault();
            objServiceRequest.Status = 2;
            try
            {
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
                //    obj.From = new MailAddress("dummy");
                //    obj.IsBodyHtml = true;
                //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                //    smtp.Port = 587;
                //    smtp.UseDefaultCredentials = true;
                //    smtp.EnableSsl = true;
                //    smtp.Credentials = new System.Net.NetworkCredential("dummy", "dummy");
                //    smtp.Send(obj);

                //}

                return true;
            }
            catch (Exception ex)
            {
                string _Message = ex.Message;
                return false;
            }
        }

        public bool CompleteServiceRequest(string serviceRequestId)
        {

            return CompletedService(Int32.Parse(serviceRequestId));
        }


        public bool CompletedService(int serviceRequestId)
        {
            ServiceRequest serviceRequest = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            serviceRequest.Status = 1;
            _context.ServiceRequests.Update(serviceRequest);
            _context.SaveChanges();
            return true;
        }

        public string AcceptServiceRequest(string serviceRequeestId)
        {
            int ServiceProviderUserID = (int)HttpContext.Session.GetInt32("userID");
            return AcceptServiceRequests(ServiceProviderUserID, Int32.Parse(serviceRequeestId));
        }

        public string AcceptServiceRequests(int userId, int serviceRequestId)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            if (sr.ServiceProviderId != null)
            {
                return "This service request is no more available. It has been assigned to another provider.";
            }
            else
            {
                ServiceRequest s = _context.ServiceRequests.Where(x => x.ServiceProviderId == userId && x.Status == 3 && x.ServiceStartDate == sr.ServiceStartDate).FirstOrDefault();
                if (s != null)
                {
                    return "Another service request  has already been assigned which has time overlap with this service request. You can’t pick this one!";
                }
                else
                {
                    sr.ServiceProviderId = userId;
                    sr.SpacceptedDate = DateTime.Now;
                    sr.Status = 3;
                    _context.ServiceRequests.Update(sr);
                    _context.SaveChanges();
                
                    return "true";
                }
            }

        }

        public IActionResult ServiceScheduleGoodToHave()
        {
            return View();
        }

        public IActionResult GetUpcomingServiceRequest()
        {
            int userId = (int)HttpContext.Session.GetInt32("userID"); ;
            return new JsonResult(GetDateOfAcceptedAndUpcomingServiceRequest(userId));
        }

        public object GetDateOfAcceptedAndUpcomingServiceRequest(int userId)
        {
            List<ServiceRequest> serviceRequestList = _context.ServiceRequests.Where(x => x.ServiceProviderId == userId && (x.Status == 1 || x.Status == 3)).ToList();
            serviceRequestList = serviceRequestList.Where(x => x.Status == 1 || x.Status == 3).ToList();
            var listOfDate = serviceRequestList.Select(x => new
            {
                id = x.ServiceRequestId,
                start = x.ServiceStartDate.ToString("dd/MM/yyyy"),
                color = x.Status == 1 ? "grey" : "#1d7a8c"

            }).ToList();
            return listOfDate;
        }
    }
}
