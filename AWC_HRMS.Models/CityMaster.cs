using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class CityMaster
    {
        public CityMaster()
        {
            CandidateMasterCurrentCities = new HashSet<CandidatePersonalDetails>();
            CandidateMasterPermanentCities = new HashSet<CandidatePersonalDetails>();
            EmployeeMasterCurrentCities = new HashSet<EmployeeMaster>();
            EmployeeMasterPermanentCities = new HashSet<EmployeeMaster>();
        }

        public int CityId { get; set; }
        public int? StateId { get; set; }
        public string? CityName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual StateMaster? State { get; set; }
        public virtual ICollection<CandidatePersonalDetails> CandidateMasterCurrentCities { get; set; }
        public virtual ICollection<CandidatePersonalDetails> CandidateMasterPermanentCities { get; set; }
        public virtual ICollection<EmployeeMaster> EmployeeMasterCurrentCities { get; set; }
        public virtual ICollection<EmployeeMaster> EmployeeMasterPermanentCities { get; set; }
    }
}
