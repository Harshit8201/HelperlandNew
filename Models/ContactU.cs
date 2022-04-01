using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Helperland.Models
{
    public partial class ContactU
    {
        [Key]
        public int ContactUsId { get; set; }
        [Required(ErrorMessage = "Please enter your Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }
        [StringLength(500)]
        public string Subject { get; set; }
        
        [StringLength(20)]
        [Required(ErrorMessage = "Please enter Phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Message")]
        public string Message { get; set; }
        [StringLength(100)]
        public string UploadFileName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        [StringLength(500)]
        public string FileName { get; set; }
    }
}
