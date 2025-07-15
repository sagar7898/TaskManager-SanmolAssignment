using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SanmolTaskManager_Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        [Remote("IsEmailUnique", "Customer", AdditionalFields = "Id", ErrorMessage = "Email already exists")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Remote("IsPhoneUnique", "Customer", AdditionalFields = "Id", ErrorMessage = "Phone number already exists")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter a valid 10-digit phone number")]
        public string Phone { get; set; }

        public bool IsDeleted { get; set; }

        // Navigation property
        [ValidateNever]
        public ICollection<TaskItem> Tasks { get; set; }
    }
}
