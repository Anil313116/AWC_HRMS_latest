using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWC_HRMS.Models
{
    public class LinkGeneration
    {
        [Required(ErrorMessage = "Candidate Name is Required")]
        [Display(Name = "Candidate Name")]
        public string? CandidateName { get; set; }
        [Required(ErrorMessage = "Candidate Email is Required")]
        [Display(Name = "Candidate Email")]
        public string? CandidateEmail { get; set; }
        [Required(ErrorMessage = "Candidate Contact Number is Required")]
        [Display(Name = "Candidate Contact Number")]

        public string? CandidateContactNumber { get; set; }
        [Required(ErrorMessage = "Candidate Date Of Joining is Required")]
        [Display(Name = "Date Of Joining")]

        public DateTime DateOfJoining { get; set; } 
       
    }
}
