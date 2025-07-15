using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanmolTaskManager_Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        [Display(Name = "Task Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Task Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a due date.")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "Task Status")]
        public string Status { get; set; } = "Pending"; // Values: Pending, Completed

        [Required]
        [Display(Name = "Priority Level")]
        public string Priority { get; set; } = "Medium"; // Values: High, Medium, Low

        public bool IsDeleted { get; set; } = false;

        [Required(ErrorMessage = "Please select a valid customer.")]
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        [ValidateNever]
        public Customer Customer { get; set; }

        // ✅ Timestamps
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
