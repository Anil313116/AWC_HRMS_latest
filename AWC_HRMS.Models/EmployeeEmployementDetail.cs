using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class EmployeeEmployementDetail
    {
        public int? SrNo { get; set; }
        public int Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? FresherExperienced { get; set; }
        public int? TotalExperiencedYear { get; set; }
        public int? TotalExperiencedMonth { get; set; }
        public int? NoOfCompany { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual EmployeeMaster? Employee { get; set; }
    }
}
