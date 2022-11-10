using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class CountryMaster
    {
        public CountryMaster()
        {
            StateMasters = new HashSet<StateMaster>();
        }

        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<StateMaster> StateMasters { get; set; }
    }
}
