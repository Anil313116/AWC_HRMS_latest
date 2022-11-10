using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AWC_HRMS.Models
{
    public class ForgotPasswordModel
    {
        [Required, Display(Name = "Registered User Name")]
        public string UserName { get; set; }

        [Required, EmailAddress, Display(Name = "Registered Email Address")]
        public string Email { get; set; }
    }
}
