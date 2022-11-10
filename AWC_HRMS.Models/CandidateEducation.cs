using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class CandidateEducation
    {
        public int? SrNo { get; set; }
        public int Id { get; set; }
        public string? CandidateId { get; set; }
        public string? _10SchoolName { get; set; }
        public string? _10BoardName { get; set; }
        public string? _10Course { get; set; }
        public string? _12Stream { get; set; }
        public string? _10Stream { get; set; }
        public string? UgStream { get; set; }
        public string? PgStream { get; set; }

        public string? _10YearOfPassing { get; set; }
        public string? _10PercentageCgpa { get; set; }
        public byte[]? _10MarkSheet { get; set; }
        public byte[]? _10DegreePassingCertificate { get; set; }
        public string? _12SchoolName { get; set; }
        public string? _12BoardName { get; set; }
        public string? _12Course { get; set; }
        public string? _12YearOfPassing { get; set; }
        public string? _12PercentageCgpa { get; set; }
        public byte[]? _12MarkSheet { get; set; }
        public byte[]? _12DegreePassingCertificate { get; set; }
        public string? UgCollegeName { get; set; }
        public string? UgUniversity { get; set; }
        public string? UgCourse { get; set; }
        public string? UgYearOfPassing { get; set; }
        public string? UgPercentageCgpa { get; set; }
        public string? UG_Degree_Name { get; set; }
        
        public Byte[]? UgMarkSheet { get; set; }
        public Byte[]? UgDegreePassingCertificate { get; set; }
        public string? PgCollegeName { get; set; }
        public string? PgUniversity { get; set; }
        public string? PgCourse { get; set; }
        public string? PgYearOfPassing { get; set; }
        public string? PgPercentageCgpa { get; set; }
        public string? PG_Degree_Name { get; set; }
        
        public Byte[]? PgMarkSheet { get; set; }
        public Byte[]? PgDegreePassingCertificate { get; set; }
        public int? LinkStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }


        public Byte[]? Aadhar_Image { get; set; }
        public Byte[]? PanCard_Image { get; set; }
        public Byte[]? Candidate_Image { get; set; }
        public virtual CandidatePersonalDetails? Candidate { get; set; }

        public virtual IFormFile? form { get; set; }
    }
}
