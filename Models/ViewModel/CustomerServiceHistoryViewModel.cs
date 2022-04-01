using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class CustomerServiceHistoryViewModel
    {
        public int ServiceRequestId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string ServiceStartDate { get; set; }
        public int? ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Distance { get; set; }
        public bool HasPets { get; set; }
        public string TotalHour { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
    }
}
