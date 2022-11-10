using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class EmployeeEducationDetail
    {
        public int? SrNo { get; set; }
        public int Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? _10SchoolName { get; set; }
        public string? _10BoardName { get; set; }
        public string? _10Course { get; set; }
        public string? _10YearOfPassing { get; set; }
        public string? _10PercentageCgpa { get; set; }
        public string? _10MarkSheet { get; set; }
        public string? _10DegreePassingCertificate { get; set; }
        public string? _12SchoolName { get; set; }
        public string? _12BoardName { get; set; }
        public string? _12Course { get; set; }
        public string? _12YearOfPassing { get; set; }
        public string? _12PercentageCgpa { get; set; }
        public string? _12MarkSheet { get; set; }
        public string? _12DegreePassingCertificate { get; set; }
        public string? UgCollegeName { get; set; }
        public string? UgUniversity { get; set; }
        public string? UgCourse { get; set; }
        public string? UgYearOfPassing { get; set; }
        public string? UgPercentageCgpa { get; set; }
        public string? UgMarkSheet { get; set; }
        public string? UgDegreePassingCertificate { get; set; }
        public string? PgCollegeName { get; set; }
        public string? PgUniversity { get; set; }
        public string? PgCourse { get; set; }
        public string? PgYearOfPassing { get; set; }
        public string? PgPercentageCgpa { get; set; }
        public string? PgMarkSheet { get; set; }
        public string? PgDegreePassingCertificate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual EmployeeMaster? Employee { get; set; }
    }
}
