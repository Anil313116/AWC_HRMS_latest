using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWC_HRMS.Models
{
    public partial class UserMaster
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        [Required(ErrorMessage = "User Id is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsInterestedInAdmin { get; set; }
        public bool IsInterestedInSuperAdmin { get; set; }
        public bool IsInterestedInHR { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }



        public virtual RoleMaster? Role { get; set; }
    }
}