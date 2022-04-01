using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Models.ViewModel
{
    public class RefundViewModel
    {
        public int ServiceRequestId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal InBalanceAmount { get; set; }
        public decimal CalculateAmount { get; set; }
        public string Message { get; set; }

    }
}
