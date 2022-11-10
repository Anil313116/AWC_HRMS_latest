using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class RoleMaster
    {
        public RoleMaster()
        {
            UserMasters = new HashSet<UserMaster>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<UserMaster> UserMasters { get; set; }
    }
}
