using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWC_HRMS.Models
{
    public partial class CandidatePersonalDetails
    {
        public CandidatePersonalDetails()
        {
            CandidateEducations = new HashSet<CandidateEducation>();
            CandidateEmployements = new HashSet<CandidateEmployement>();


            stateMasters = new List<StateMaster>();
            stateMasters.Add(new StateMaster
            {
                   StateId = 0,
                    StateName = "--All--"
                });

      
        cityMasters = new List<CityMaster>();
            cityMasters.Add(new CityMaster
            {
                   CityId = 0,
                    CityName = "--All--"
                });

    }

        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Candidate Id")]
        public string CandidateId { get; set; } = null!;
        public string? CandidateName { get; set; }
        public string? CandidateEmailId { get; set; }
        [StringLength(10)]
        public string? CandidateContactNumber { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        public byte? CandidatePhoto { get; set; }
       
        public string? CandidateAlternateNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public DateTime? AnniversaryDate { get; set; }
        
        public string Reason { get; set; }
        public string? MaritalStatus { get; set; }
        public string? FatherName { get; set; }
        public string? PermanentAddress { get; set; }
        public int? PermanentStateId { get; set; }
        public int? PermanentCityId { get; set; }
        public string? PermanentOtherState { get; set; }

        public string? PermanentOtherCity { get; set; }


        public string? PermanentPinCode { get; set; }
        public string? CurrentAddress { get; set; }
        public int? CurrentStateId { get; set; }
        public int? CurrentCityId { get; set; }
        public string? CurrentOtherState { get; set; }

        public string? CurrentOtherCity { get; set; }

        public string? CurrentPinCode { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? AadharNumber { get; set; }
        public string? PanNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? UANNumber { get; set; }
        public DateTime? LastpfDate { get; set; }

        public byte[]? CandidateImage { get; set; }
        public byte[]? AadharImage { get; set; }
        public byte[]? PanCardImage { get; set; }
        public byte[]? PassportImage { get; set; }
        public string? BankAccountHolderNamePri { get; set; }
        public string? BankNamePri { get; set; }
        public string? BankAccountNumberPri { get; set; }
        public string? IfscCodePri { get; set; }
        public string? BankBranchAddressPri { get; set; }
        public byte[] PassBookCancelChequePri { get; set; }


        public string? BankAccountHolderNameSec { get; set; }
        public string? BankNameSec { get; set; }
        public string? BankAccountNumberSec { get; set; }
        public string? IfscCodeSec { get; set; }
        public string? BankBranchAddressSec { get; set; }
        public byte[] PassBookCancelChequeSec { get; set; }
        //public string? PermanentState { get; set; }
        public bool? IsActive { get; set; }
        public int? LinkStatus { get; set; }
        public byte[] Certificate10 { get; set; }
        public byte[] MarkSheet10 { get; set; }

        public byte[] Certificate12 { get; set; }
        public byte[] MarkSheet12 { get; set; }

        public byte[] DegreeUG { get; set; }
        public byte[] MarkSheetUG { get; set; }

        public byte[] DegreePG { get; set; }
        public byte[] MarkSheetPG { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual CityMaster? CurrentCity { get; set; }
        public virtual StateMaster? CurrentState { get; set; }
        public virtual CityMaster? PermanentCity { get; set; }
        public virtual StateMaster? PermanentState { get; set; }

        public List<CityMaster> cityMasters { get; set; }

        public List<StateMaster> stateMasters { get; set; }
        public virtual ICollection<CandidateEducation> CandidateEducations { get; set; }
        public virtual ICollection<CandidateEmployement> CandidateEmployements { get; set; }
        public virtual IFormFile? form { get; set; }
    }
}
