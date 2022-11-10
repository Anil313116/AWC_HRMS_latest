using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public  class EmployeeMaster
    {
        public EmployeeMaster()
        {
            EmployeeEducationDetails = new HashSet<EmployeeEducationDetail>();
            EmployeeEmployementDetails = new HashSet<EmployeeEmployementDetail>();
        }

        public int Id { get; set; }
        public string Employee_Id { get; set; } = null!;
        public string? Employee_Name { get; set; }
        public string? Gender { get; set; }
        public string? EmpPhoto { get; set; }
        public string? EmpPhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public string? MaritalStatus { get; set; }
        public string? FatherName { get; set; }
        public string? PermanentAddress { get; set; }
        public int? PermanentStateId { get; set; }
        public int? PermanentCityId { get; set; }
        public string? PermanentPinCode { get; set; }
        public string? CurrentAddress { get; set; }
        public int? CurrentStateId { get; set; }
        public int? CurrentCityId { get; set; }
        public string? CurrentPinCode { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? AadharNumber { get; set; }
        public string? PanNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? EmployeeImage { get; set; }
        public string? AadharImage { get; set; }
        public string? PanCardImage { get; set; }
        public string? PassportImage { get; set; }
        public string? BankAccountHolderName { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IfscCode { get; set; }
        public string? BankBranchAddress { get; set; }
        public string? PassBookCancelCheque { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual CityMaster? CurrentCity { get; set; }
        public virtual StateMaster? CurrentState { get; set; }
        public virtual CityMaster? PermanentCity { get; set; }
        public virtual StateMaster? PermanentState { get; set; }
        public virtual ICollection<EmployeeEducationDetail> EmployeeEducationDetails { get; set; }
        public virtual ICollection<EmployeeEmployementDetail> EmployeeEmployementDetails { get; set; }
    }
}
