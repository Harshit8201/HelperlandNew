using Helperland.Data;
using Helperland.Models;
using Helperland.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AdminPortionController : Controller
    {
        private readonly HelperlandContext _context;
        public AdminPortionController(HelperlandContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.city = GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            AdminServiceHistoryViewModel model = new AdminServiceHistoryViewModel();
            model.ServiceHistory = GetAllServiceListForAdmin();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(AdminServiceHistoryViewModel serchData)
        {
            ViewBag.city = GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            AdminServiceHistoryViewModel model = new AdminServiceHistoryViewModel();
            model.ServiceHistory = GetServiceBySearchs(serchData);
            return View(model);
        }

        public List<ServiceRequestAdminViewModel> GetServiceBySearchs(AdminServiceHistoryViewModel li)
        {
            List<ServiceRequest> sr = _context.ServiceRequests.Include(x => x.ServiceRequestAddresses).ToList();
            List<ServiceRequestAdminViewModel> list = new List<ServiceRequestAdminViewModel>();
            SearchViewModel searchViewModel = li.SearchViewModel;
            if (searchViewModel.ServiceId != null)
            {
                sr = sr.Where(x => x.ServiceRequestId == searchViewModel.ServiceId).ToList();
            }
            if (searchViewModel.PostalCode != null)
            {
                sr = sr.Where(x => x.ZipCode.Contains(searchViewModel.PostalCode, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (searchViewModel.ToDate != null)
            {
                DateTime todate = Convert.ToDateTime(searchViewModel.ToDate);
                sr = sr.Where(x => x.ServiceStartDate < todate).ToList();
            }
            if (searchViewModel.FromDate != null)
            {
                DateTime fromDate = Convert.ToDateTime(searchViewModel.FromDate);
                sr = sr.Where(x => x.ServiceStartDate > fromDate).ToList();
            }
            if (searchViewModel.Status != -1)
            {
                sr = sr.Where(x => x.Status == searchViewModel.Status).ToList();
            }
            bool check = false;
            bool isInside = false;
            foreach (var item in sr)
            {
                check = false;
                ServiceRequestAdminViewModel temp = new ServiceRequestAdminViewModel();
                User Customer = _context.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                if (item.ServiceProviderId != null)
                {
                    User ServiceProvider = _context.Users.Where(x => x.UserId == item.ServiceProviderId).FirstOrDefault();
                    temp.ServiceRequestId = (int)item.ServiceProviderId;
                    temp.ServiceProviderName = ServiceProvider.FirstName + " " + ServiceProvider.LastName;
                    temp.Rating = (int)GetRating(item.ServiceProviderId);
                }

                temp.ServiceRequestId = item.ServiceRequestId;
                temp.ServiceDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                temp.CustomerName = Customer.FirstName + " " + Customer.LastName;
                UserAddress address = _context.UserAddresses.Where(x => x.UserId == item.UserId).FirstOrDefault();
                temp.CustomerAddressLine1 = address.AddressLine1;
                temp.CustomerAddressLine2 = address.AddressLine2;
                temp.NetAmount = item.TotalCost;
                temp.Status = item.Status;
                if (searchViewModel.CustomerName != null)
                {
                    isInside = true;
                    if (Customer.FirstName.Contains(searchViewModel.CustomerName, System.StringComparison.CurrentCultureIgnoreCase) || Customer.LastName.Contains(searchViewModel.CustomerName, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        check = true;
                    }

                }
                if (searchViewModel.ServiceProviderName != null)
                {
                    if (temp.ServiceProviderName != null)
                    {
                        isInside = true;
                        if (temp.ServiceProviderName.Contains(searchViewModel.ServiceProviderName, System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            check = true;
                        }
                    }
                }
                if (searchViewModel.Email != null)
                {
                    isInside = true;
                    if (Customer.Email.Contains(searchViewModel.Email, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        check = true;
                    }

                }
                if (isInside == true && check == true)
                {
                    list.Add(temp);
                }
                if (!isInside)
                {
                    list.Add(temp);
                }

            }

            return list;
        }
        public IActionResult GetServiceRequestData(string ServiceReqestId)
        {

            return Json(JsonConvert.SerializeObject(GetServiceDetailss(Int32.Parse(ServiceReqestId))));
        }


        public object GetServiceDetailss(int serviceRequestId)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            List<ServiceRequestExtra> sre = _context.ServiceRequestExtras.Where(x => x.ServiceRequestId == serviceRequestId).ToList();
            ServiceRequestAddress sra = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            string name = "";
            string avatar = "";
            User u = _context.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault();
            if (sr.ServiceProviderId != null)
            {
                User user = _context.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();
                name = user.FirstName + " " + user.LastName;
                avatar = user.UserProfilePicture;
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
                Rating = GetRating(sr.ServiceProviderId),
                Avatar = avatar
            };
            return temp;

        }
        public IActionResult EditService(AdminServiceHistoryViewModel editData)
        {
            if (ModelState.IsValid)
            {
                UpdateServiceRequests(editData.EditServiceModel);
            }

            ViewBag.city = GetAllCity().Select(x => new SelectListItem()
            {
                Text = x.CityName,
                Value = x.Id.ToString()
            });
            return RedirectToAction("Index");

        }

        public void UpdateServiceRequests(EditServiceRequestViewModel editServiceRequestViewModel)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == editServiceRequestViewModel.ServiceRequestId).FirstOrDefault();
            ServiceRequestAddress sra = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == editServiceRequestViewModel.ServiceRequestId).FirstOrDefault();
            bool isDateChange = false;
            if (editServiceRequestViewModel.ServiceStartDate != sr.ServiceStartDate)
            {
                isDateChange = true;
            }
            sr.ServiceStartDate = editServiceRequestViewModel.ServiceStartDate;
            sra.AddressLine1 = editServiceRequestViewModel.StreetName;
            sra.AddressLine2 = editServiceRequestViewModel.HouseNumber;
            sra.City = editServiceRequestViewModel.CityId.ToString();
            sra.PostalCode = editServiceRequestViewModel.Postalcode;
            _context.ServiceRequests.Update(sr);
            _context.ServiceRequestAddresses.Update(sra);
            _context.SaveChanges();
            

        }

        public IActionResult GetServiceRequestDetailsForEdit(string ServiceReqestId)
        {

            return Json(JsonConvert.SerializeObject(GetServiceDetailsForAdmin(Int32.Parse(ServiceReqestId))));
        }

        public object GetServiceDetailsForAdmin(int serviceRequestId)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();
            ServiceRequestAddress sra = _context.ServiceRequestAddresses.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            var temp = new
            {
                ServiceDate = sr.ServiceStartDate.ToString("dd/MM/yyyy"),
                StreetName = sra.AddressLine1,
                HouseNumber = sra.AddressLine2,
                Zipcode = sra.PostalCode,
                CityId = sra.City
            };
            return temp;

        }

        public IActionResult GetRefundDetails(string serviceRequestId)
        {
            return Json(JsonConvert.SerializeObject(GetRefundDetailss(Int32.Parse(serviceRequestId))));
        }

        public object GetRefundDetailss(int serviceRequestId)
        {
            ServiceRequest sr = _context.ServiceRequests.Where(x => x.ServiceRequestId == serviceRequestId).FirstOrDefault();

            decimal refundAmount;
            if (sr.RefundedAmount == null)
            {
                refundAmount = 0;
            }
            else
                refundAmount = (decimal)sr.RefundedAmount;

            var temp = new
            {
                TotalAmount = sr.TotalCost,
                RefundAmount = refundAmount,

            };
            return temp;
        }

        public List<ServiceRequestAdminViewModel> GetAllServiceListForAdmin()
        {
            List<ServiceRequest> sr = _context.ServiceRequests.Include(x => x.ServiceRequestAddresses).ToList();

            List<ServiceRequestAdminViewModel> list = new List<ServiceRequestAdminViewModel>();
            foreach (var item in sr)
            {
                ServiceRequestAdminViewModel temp = new ServiceRequestAdminViewModel();
                User Customer = _context.Users.Where(x => x.UserId == item.UserId).FirstOrDefault();
                if (item.ServiceProviderId != null)
                {
                    User ServiceProvider = _context.Users.Where(x => x.UserId == item.ServiceProviderId).FirstOrDefault();
                    temp.ServiceRequestId = (int)item.ServiceProviderId;
                    temp.ServiceProviderName = ServiceProvider.FirstName + " " + ServiceProvider.LastName;
                    temp.Rating = (int)GetRating(item.ServiceProviderId);
                    temp.Avatar = ServiceProvider.UserProfilePicture;
                }

                temp.ServiceRequestId = item.ServiceRequestId;
                temp.ServiceDate = item.ServiceStartDate.ToString("dd/MM/yyyy");
                temp.CustomerName = Customer.FirstName + " " + Customer.LastName;
                UserAddress address = _context.UserAddresses.Where(x => x.UserId == item.UserId).FirstOrDefault();
                temp.CustomerAddressLine1 = address.AddressLine1;
                temp.CustomerAddressLine2 = address.AddressLine2;
                temp.NetAmount = item.TotalCost;
                temp.Status = item.Status;

                list.Add(temp);
            }

            return list;
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
                    return Decimal.Round(sum / i);
                }

            }
        }

        public List<City> GetAllCity()
        {
            return _context.Cities.ToList();
        }





        public IActionResult UserManagement()
        {
            AdminServiceHistoryViewModel model = new AdminServiceHistoryViewModel();
            model.UserList = GetAllUser();
            return View(model);
        }



        public List<UserManagementViewModel> GetAllUser()
        {

            IEnumerable<User>  users = _context.Users.Where(x => x.UserTypeId != 2).ToList();


            List<UserManagementViewModel> list = new List<UserManagementViewModel>();
            foreach (var item in users)
            {
                UserManagementViewModel temp = new UserManagementViewModel();
                temp.UserId = item.UserId;
                temp.UserName = item.FirstName + " " + item.LastName;
                temp.RegistrationDate = item.CreatedDate.ToString();
                
                if (item.UserTypeId == 0)
                {
                    temp.UserType = "Customer";
                }
                else if (item.UserTypeId == 1)
                {
                    temp.UserType = "Service Provider";
                    temp.IsApprove = item.IsApproved;
                }
                temp.UserTypeId = item.UserTypeId;
                temp.PhoneNumber = item.Mobile;
                temp.PostalCode = item.ZipCode;
                temp.Status = item.IsActive;
                list.Add(temp);
            }
            return list;
        }



        [HttpPost]
        public IActionResult UserManagement(AdminServiceHistoryViewModel serchData)
        {
            AdminServiceHistoryViewModel model = new AdminServiceHistoryViewModel();
            model.UserList = GetFilterUserList(serchData.UserSearchModel);
            return View(model);
        }


        public List<UserManagementViewModel> GetFilterUserList(UserSearchViewModel userSearchViewModel)
        {
            List<User> users = _context.Users.Where(x => x.UserTypeId != 3).ToList();
            List<UserManagementViewModel> list = new List<UserManagementViewModel>();
            if (userSearchViewModel.Zipcode != null)
            {
                users = users.Where(x => x.ZipCode != null && x.ZipCode.Contains(userSearchViewModel.Zipcode, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (userSearchViewModel.Username != null)
            {
                users = users.Where(x => x.FirstName.Contains(userSearchViewModel.Username, System.StringComparison.CurrentCultureIgnoreCase) || x.LastName.Contains(userSearchViewModel.Username, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (userSearchViewModel.UserType != -1)
            {
                users = users.Where(x => x.UserTypeId == userSearchViewModel.UserType).ToList();
            }
            if (userSearchViewModel.PhoneNumber != null)
            {
                users = users.Where(x => x.Mobile != null && x.Mobile.Contains(userSearchViewModel.PhoneNumber, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            foreach (var item in users)
            {
                UserManagementViewModel temp = new UserManagementViewModel();
                temp.UserId = item.UserId;
                temp.UserName = item.FirstName + " " + item.LastName;
                temp.RegistrationDate = item.CreatedDate.ToString(/*"dd/MM/yyyy"*/);
                if (item.UserTypeId == 1)
                {
                    temp.UserType = "Customer";
                }
                else if (item.UserTypeId == 2)
                {
                    temp.UserType = "Service Provider";
                }
                temp.PhoneNumber = item.Mobile;
                temp.PostalCode = item.ZipCode;
                temp.Status = item.IsActive;
                list.Add(temp);
            }
            return list;
        }




        public bool DeleteUser(string userId)
        {
            return DeleteUsers(Int32.Parse(userId));
        }

        public bool DeleteUsers(int userId)
        {
            User user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ActivateUser(string userId)
        {
            return ActivateUsers(Int32.Parse(userId));
        }

        public bool ActivateUsers(int userId)
        {
            User user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsActive = true;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeactivateUser(string userId)
        {
            return DeactivateUsers(Int32.Parse(userId));
        }

        public bool DeactivateUsers(int userId)
        {
            User user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsActive = false;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ApproveServiceProvider(string userId)
        {
            return ApproveServiceProviders(Int32.Parse(userId));
        }

        public bool ApproveServiceProviders(int userId)
        {
            User user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsApproved = true;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DisApproveServiceProvider(string userId)
        {
            return DisApproveServiceProviders(Int32.Parse(userId));
        }

        public bool DisApproveServiceProviders(int userId)
        {
            User user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            try
            {
                user.IsApproved = false;
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public IActionResult RefundAmount(AdminServiceHistoryViewModel refund)
        {

            RefundAmounts(refund.RefundModel);
            return RedirectToAction("Index");
        }

        public void RefundAmounts(RefundViewModel refundViewModel)
        {
            ServiceRequest serviceRequest = _context.ServiceRequests.Where(x => x.ServiceRequestId == refundViewModel.ServiceRequestId).FirstOrDefault();
            if (serviceRequest.RefundedAmount == null)
            {
                serviceRequest.RefundedAmount = refundViewModel.CalculateAmount;
            }
            else
            {
                serviceRequest.RefundedAmount += refundViewModel.CalculateAmount;
            }
            _context.ServiceRequests.Update(serviceRequest);
            _context.SaveChanges();
           


        }

    }
}
