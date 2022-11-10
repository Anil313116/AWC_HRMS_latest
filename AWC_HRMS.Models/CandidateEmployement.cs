using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWC_HRMS.Models
{
    public partial class CandidateEmployement
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }


        public int? SrNo { get; set; }
        public int Id { get; set; }
        public int? CandidateId { get; set; }
        public string? FresherExperienced { get; set; }
       
        public int? TotalExperiencedYear { get; set; }
        public int? TotalExperiencedMonth { get; set; }
        public int? NoOfCompany { get; set; }

        public string? NameOfCompany { get; set; }

        public string? Designation { get; set; }

        public string? DOJ { get; set; }

        public string? DOL { get; set; }
        public string? CTC { get; set; }
        public string? RelatedDocument { get; set; }
        public string? Reason { get; set; }

        public int? LinkStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual CandidatePersonalDetails? Candidate { get; set; }
    }
}
