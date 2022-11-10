using System;
using System.Collections.Generic;
using AWC_HRMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AWC_HRMS.Models
{
    public partial class AWC_HRMSContext : DbContext
    {
        public AWC_HRMSContext()
        {
        }

        public AWC_HRMSContext(DbContextOptions<AWC_HRMSContext> options)
            : base(options)
        {
        }

        public DbSet<CandidateEducation> CandidateEducations { get; set; } = null!;
        public DbSet<CandidateEmployement> CandidateEmployements { get; set; } = null!;
        public DbSet<CandidatePersonalDetails> CandidateMasters { get; set; } = null!;
        public DbSet<CityMaster> CityMasters { get; set; } = null!;
        public DbSet<CountryMaster> CountryMasters { get; set; } = null!;
        public DbSet<EmployeeEducationDetail> EmployeeEducationDetails { get; set; } = null!;
        public DbSet<EmployeeEmployementDetail> EmployeeEmployementDetails { get; set; } = null!;
        public DbSet<EmployeeMaster> EmployeeMasters { get; set; } = null!;
        public DbSet<LinkGenerationTable> LinkGenerationTables { get; set; } = null!;
        public DbSet<RoleMaster> RoleMasters { get; set; } = null!;
        public DbSet<StateMaster> StateMasters { get; set; } = null!;
        public DbSet<UserMaster> UserMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-IEQPJ2N; Database=AWC_HRMS; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateEducation>(entity =>
            {
                entity.ToTable("Candidate_Education");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CandidateId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Candidate_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.PgCollegeName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PG_College_Name");

                entity.Property(e => e.PgCourse).HasColumnName("PG_Course");

                entity.Property(e => e.PgDegreePassingCertificate).HasColumnName("PG_Degree/Passing_Certificate");

                entity.Property(e => e.PgMarkSheet).HasColumnName("PG_MarkSheet");

                entity.Property(e => e.PgPercentageCgpa)
                    .HasColumnType("datetime")
                    .HasColumnName("PG_Percentage/CGPA");

                entity.Property(e => e.PgUniversity).HasColumnName("PG_University");

                entity.Property(e => e.PgYearOfPassing)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PG_Year_of_Passing");

                entity.Property(e => e.SrNo).HasColumnName("Sr_No.");

                entity.Property(e => e.UgCollegeName).HasColumnName("UG_College_Name");

                entity.Property(e => e.UgCourse)
                    .HasMaxLength(100)
                    .HasColumnName("UG_Course");

                entity.Property(e => e.UgDegreePassingCertificate)
                    .HasMaxLength(10)
                    .HasColumnName("UG_Degree/Passing_Certificate");

                entity.Property(e => e.UgMarkSheet)
                    .HasMaxLength(150)
                    .HasColumnName("UG_MarkSheet");

                entity.Property(e => e.UgPercentageCgpa)
                    .HasMaxLength(200)
                    .HasColumnName("UG_Percentage/CGPA");

                entity.Property(e => e.UgUniversity).HasColumnName("UG_University");

                entity.Property(e => e.UgYearOfPassing)
                    .HasMaxLength(250)
                    .HasColumnName("UG_Year_of_Passing");

                entity.Property(e => e._10BoardName)
                    .HasMaxLength(250)
                    .HasColumnName("10_Board_Name");

                entity.Property(e => e._10Course)
                    .HasMaxLength(200)
                    .HasColumnName("10_Course");

                entity.Property(e => e._10DegreePassingCertificate).HasColumnName("10_Degree/Passing_Certificate");

                entity.Property(e => e._10MarkSheet)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("10_MarkSheet");

                entity.Property(e => e._10PercentageCgpa)
                    .HasMaxLength(10)
                    .HasColumnName("10_Percentage/CGPA");

                entity.Property(e => e._10SchoolName)
                    .HasMaxLength(100)
                    .HasColumnName("10_School _Name");

                entity.Property(e => e._10YearOfPassing)
                    .HasMaxLength(150)
                    .HasColumnName("10_Year_of _Passing");

                entity.Property(e => e._12BoardName)
                    .HasMaxLength(100)
                    .HasColumnName("12_Board_Name");

                entity.Property(e => e._12Course)
                    .HasMaxLength(250)
                    .HasColumnName("12_Course");

                entity.Property(e => e._12DegreePassingCertificate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("12_Degree/Passing_Certificate");

                entity.Property(e => e._12MarkSheet)
                    .HasMaxLength(10)
                    .HasColumnName("12_MarkSheet");

                entity.Property(e => e._12PercentageCgpa)
                    .HasMaxLength(150)
                    .HasColumnName("12_Percentage/CGPA");

                entity.Property(e => e._12SchoolName).HasColumnName("12_School_Name");

                entity.Property(e => e._12YearOfPassing)
                    .HasMaxLength(200)
                    .HasColumnName("12_Year_of_Passing");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateEducations)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK__Candidate__Candi__787EE5A0");
            });

            modelBuilder.Entity<CandidateEmployement>(entity =>
            {
                entity.ToTable("Candidate_Employement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CandidateId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Candidate_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FresherExperienced)
                    .HasMaxLength(15)
                    .HasColumnName("Fresher/Experienced");

                entity.Property(e => e.NoOfCompany).HasColumnName("No_of_Company");

                entity.Property(e => e.SrNo).HasColumnName("Sr_No.");

                entity.Property(e => e.TotalExperiencedMonth).HasColumnName("Total_Experienced_Month");

                entity.Property(e => e.TotalExperiencedYear).HasColumnName("Total_Experienced_Year");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateEmployements)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK__Candidate__Candi__7B5B524B");
            });

            modelBuilder.Entity<CandidatePersonalDetails>(entity =>
            {
                entity.HasKey(e => e.CandidateId)
                    .HasName("PK__Candidat__67F75EFD37FA6B60");

                entity.ToTable("Candidate_Master");

                entity.Property(e => e.CandidateId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Candidate_Id");

                entity.Property(e => e.AadharImage).HasColumnName("Aadhar_Image");

                entity.Property(e => e.AadharNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Aadhar_Number");

                entity.Property(e => e.BankAccountHolderNamePri)
                    .HasMaxLength(100)
                    .HasColumnName("Bank_Account_Holder_Name");

                entity.Property(e => e.BankAccountNumberPri)
                    .HasMaxLength(20)
                    .HasColumnName("Bank_Account_Number");

                entity.Property(e => e.BankBranchAddressPri).HasColumnName("Bank_Branch_Address");

                entity.Property(e => e.BankNamePri)
                    .HasMaxLength(100)
                    .HasColumnName("Bank_Name");

                entity.Property(e => e.CandidateContactNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Candidate_Contact_Number");

                entity.Property(e => e.CandidateEmailId).HasColumnName("Candidate_Email_Id");

                entity.Property(e => e.CandidateImage).HasColumnName("Candidate_Image");

                entity.Property(e => e.CandidateName)
                    .HasMaxLength(100)
                    .HasColumnName("Candidate_Name");

                entity.Property(e => e.CandidateAlternateNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Candidate_AlternateNumber");

                entity.Property(e => e.CandidatePhoto).HasColumnName("Candidate_Photo");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrentAddress).HasColumnName("Current_Address");

                entity.Property(e => e.CurrentCityId).HasColumnName("Current_City_ID");

                entity.Property(e => e.CurrentPinCode)
                    .HasMaxLength(10)
                    .HasColumnName("Current_PinCode");

                entity.Property(e => e.CurrentStateId).HasColumnName("Current_State_ID");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmergencyContactName)
                    .HasMaxLength(100)
                    .HasColumnName("Emergency_Contact_Name");

                entity.Property(e => e.EmergencyContactNo)
                    .HasMaxLength(15)
                    .HasColumnName("Emergency_Contact_No");

                entity.Property(e => e.EmergencyContactRelation)
                    .HasMaxLength(40)
                    .HasColumnName("Emergency_Contact_Relation");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(100)
                    .HasColumnName("Father_Name");

                entity.Property(e => e.Gender).HasMaxLength(15);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IfscCodePri)
                    .HasMaxLength(15)
                    .HasColumnName("IFSC_Code");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.PanCardImage).HasColumnName("PanCard_Image");

                entity.Property(e => e.PanNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Pan_Number");

                entity.Property(e => e.PassBookCancelChequePri).HasColumnName("PassBook/Cancel_Cheque");

                entity.Property(e => e.PassportImage).HasColumnName("Passport_Image");

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Passport_Number");

                entity.Property(e => e.PermanentAddress).HasColumnName("Permanent_Address");

                entity.Property(e => e.PermanentCityId).HasColumnName("Permanent_City_ID");

                entity.Property(e => e.PermanentPinCode)
                    .HasMaxLength(10)
                    .HasColumnName("Permanent_PinCode");

                entity.Property(e => e.PermanentStateId).HasColumnName("Permanent_State_ID");

                entity.HasOne(d => d.CurrentCity)
                    .WithMany(p => p.CandidateMasterCurrentCities)
                    .HasForeignKey(d => d.CurrentCityId)
                    .HasConstraintName("FK__Candidate__Curre__75A278F5");

                entity.HasOne(d => d.CurrentState)
                    .WithMany(p => p.CandidateMasterCurrentStates)
                    .HasForeignKey(d => d.CurrentStateId)
                    .HasConstraintName("FK__Candidate__Curre__74AE54BC");

                entity.HasOne(d => d.PermanentCity)
                    .WithMany(p => p.CandidateMasterPermanentCities)
                    .HasForeignKey(d => d.PermanentCityId)
                    .HasConstraintName("FK__Candidate__Perma__73BA3083");

                entity.HasOne(d => d.PermanentState)
                    .WithMany(p => p.CandidateMasterPermanentStates)
                    .HasForeignKey(d => d.PermanentStateId)
                    .HasConstraintName("FK__Candidate__Perma__72C60C4A");
            });

            modelBuilder.Entity<CityMaster>(entity =>
            {
                entity.HasKey(e => e.CityId)
                    .HasName("PK__City_Mas__DE9DE000B7A43CF9");

                entity.ToTable("City_Master");

                entity.Property(e => e.CityId).HasColumnName("City_Id");

                entity.Property(e => e.CityName)
                    .HasMaxLength(200)
                    .HasColumnName("City_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.StateId).HasColumnName("State_Id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.CityMasters)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__City_Mast__State__6A30C649");
            });

            modelBuilder.Entity<CountryMaster>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__Country___8036CBAE00C1EF1C");

                entity.ToTable("Country_master");

                entity.Property(e => e.CountryId)
                    .ValueGeneratedNever()
                    .HasColumnName("Country_Id");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(100)
                    .HasColumnName("Country_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmployeeEducationDetail>(entity =>
            {
                entity.ToTable("Employee_Education_Details");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PgCollegeName)
                    .HasMaxLength(250)
                    .HasColumnName("PG_College_Name");

                entity.Property(e => e.PgCourse)
                    .HasMaxLength(150)
                    .HasColumnName("PG_Course");

                entity.Property(e => e.PgDegreePassingCertificate).HasColumnName("PG_Degree/Passing_Certificate");

                entity.Property(e => e.PgMarkSheet).HasColumnName("PG_MarkSheet");

                entity.Property(e => e.PgPercentageCgpa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PG_Percentage/CGPA");

                entity.Property(e => e.PgUniversity)
                    .HasMaxLength(200)
                    .HasColumnName("PG_University");

                entity.Property(e => e.PgYearOfPassing)
                    .HasMaxLength(10)
                    .HasColumnName("PG_Year_of_Passing");

                entity.Property(e => e.SrNo).HasColumnName("Sr_No.");

                entity.Property(e => e.UgCollegeName)
                    .HasMaxLength(250)
                    .HasColumnName("UG_College_Name");

                entity.Property(e => e.UgCourse)
                    .HasMaxLength(150)
                    .HasColumnName("UG_Course");

                entity.Property(e => e.UgDegreePassingCertificate).HasColumnName("UG_Degree/Passing_Certificate");

                entity.Property(e => e.UgMarkSheet).HasColumnName("UG_MarkSheet");

                entity.Property(e => e.UgPercentageCgpa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UG_Percentage/CGPA");

                entity.Property(e => e.UgUniversity)
                    .HasMaxLength(200)
                    .HasColumnName("UG_University");

                entity.Property(e => e.UgYearOfPassing)
                    .HasMaxLength(10)
                    .HasColumnName("UG_Year_of_Passing");

                entity.Property(e => e._10BoardName)
                    .HasMaxLength(200)
                    .HasColumnName("10_Board_Name");

                entity.Property(e => e._10Course)
                    .HasMaxLength(300)
                    .HasColumnName("10_Course");

                entity.Property(e => e._10DegreePassingCertificate).HasColumnName("10_Degree/Passing_Certificate");

                entity.Property(e => e._10MarkSheet).HasColumnName("10_MarkSheet");

                entity.Property(e => e._10PercentageCgpa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("10_Percentage/CGPA");

                entity.Property(e => e._10SchoolName)
                    .HasMaxLength(250)
                    .HasColumnName("10_School _Name");

                entity.Property(e => e._10YearOfPassing)
                    .HasMaxLength(10)
                    .HasColumnName("10_Year_of _Passing");

                entity.Property(e => e._12BoardName)
                    .HasMaxLength(200)
                    .HasColumnName("12_Board_Name");

                entity.Property(e => e._12Course)
                    .HasMaxLength(300)
                    .HasColumnName("12_Course");

                entity.Property(e => e._12DegreePassingCertificate).HasColumnName("12_Degree/Passing_Certificate");

                entity.Property(e => e._12MarkSheet).HasColumnName("12_MarkSheet");

                entity.Property(e => e._12PercentageCgpa)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("12_Percentage/CGPA");

                entity.Property(e => e._12SchoolName)
                    .HasMaxLength(250)
                    .HasColumnName("12_School _Name");

                entity.Property(e => e._12YearOfPassing)
                    .HasMaxLength(10)
                    .HasColumnName("12_Year_of_Passing");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEducationDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Employee___Emplo__7E37BEF6");
            });

            modelBuilder.Entity<EmployeeEmployementDetail>(entity =>
            {
                entity.ToTable("Employee_Employement_Details");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.FresherExperienced)
                    .HasMaxLength(15)
                    .HasColumnName("Fresher/Experienced");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NoOfCompany).HasColumnName("No_of_Company");

                entity.Property(e => e.SrNo).HasColumnName("Sr_No.");

                entity.Property(e => e.TotalExperiencedMonth).HasColumnName("Total_Experienced_Month");

                entity.Property(e => e.TotalExperiencedYear).HasColumnName("Total_Experienced_Year");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeEmployementDetails)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Employee___Emplo__01142BA1");
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.HasKey(e => e.Employee_Id)
                    .HasName("PK__Employee__781134A1D0A972C4");

                entity.ToTable("Employee_Master");

                entity.Property(e => e.Employee_Id)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Id");

                entity.Property(e => e.AadharImage).HasColumnName("Aadhar_Image");

                entity.Property(e => e.AadharNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Aadhar_Number");

                entity.Property(e => e.BankAccountHolderName)
                    .HasMaxLength(100)
                    .HasColumnName("Bank_Account_Holder_Name");

                entity.Property(e => e.BankAccountNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Bank_Account_Number");

                entity.Property(e => e.BankBranchAddress).HasColumnName("Bank_Branch_Address");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .HasColumnName("Bank_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.CurrentAddress).HasColumnName("Current_Address");

                entity.Property(e => e.CurrentCityId).HasColumnName("Current_City_ID");

                entity.Property(e => e.CurrentPinCode)
                    .HasMaxLength(10)
                    .HasColumnName("Current_PinCode");

                entity.Property(e => e.CurrentStateId).HasColumnName("Current_State_ID");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmergencyContactName)
                    .HasMaxLength(100)
                    .HasColumnName("Emergency_Contact_Name");

                entity.Property(e => e.EmergencyContactNo)
                    .HasMaxLength(15)
                    .HasColumnName("Emergency_Contact_No");

                entity.Property(e => e.EmergencyContactRelation)
                    .HasMaxLength(40)
                    .HasColumnName("Emergency_Contact_Relation");

                entity.Property(e => e.EmpPhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Emp_Phone_Number");

                entity.Property(e => e.EmpPhoto).HasColumnName("Emp_Photo");

                entity.Property(e => e.EmployeeImage).HasColumnName("Employee_Image");

                entity.Property(e => e.Employee_Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Employee_Name");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(100)
                    .HasColumnName("Father_Name");

                entity.Property(e => e.Gender).HasMaxLength(15);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IfscCode)
                    .HasMaxLength(15)
                    .HasColumnName("IFSC_Code");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.PanCardImage).HasColumnName("PanCard_Image");

                entity.Property(e => e.PanNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Pan_Number");

                entity.Property(e => e.PassBookCancelCheque).HasColumnName("PassBook/Cancel_Cheque");

                entity.Property(e => e.PassportImage).HasColumnName("Passport_Image");

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(20)
                    .HasColumnName("Passport_Number");

                entity.Property(e => e.PermanentAddress).HasColumnName("Permanent_Address");

                entity.Property(e => e.PermanentCityId).HasColumnName("Permanent_City_ID");

                entity.Property(e => e.PermanentPinCode)
                    .HasMaxLength(10)
                    .HasColumnName("Permanent_PinCode");

                entity.Property(e => e.PermanentStateId).HasColumnName("Permanent_State_ID");

                entity.HasOne(d => d.CurrentCity)
                    .WithMany(p => p.EmployeeMasterCurrentCities)
                    .HasForeignKey(d => d.CurrentCityId)
                    .HasConstraintName("FK__Employee___Curre__160F4887");

                entity.HasOne(d => d.CurrentState)
                    .WithMany(p => p.EmployeeMasterCurrentStates)
                    .HasForeignKey(d => d.CurrentStateId)
                    .HasConstraintName("FK__Employee___Curre__151B244E");

                entity.HasOne(d => d.PermanentCity)
                    .WithMany(p => p.EmployeeMasterPermanentCities)
                    .HasForeignKey(d => d.PermanentCityId)
                    .HasConstraintName("FK__Employee___Perma__17F790F9");

                entity.HasOne(d => d.PermanentState)
                    .WithMany(p => p.EmployeeMasterPermanentStates)
                    .HasForeignKey(d => d.PermanentStateId)
                    .HasConstraintName("FK__Employee___Perma__17036CC0");
            });

            modelBuilder.Entity<LinkGenerationTable>(entity =>
            {
                entity.HasKey(e => e.CandidateId)
                    .HasName("PK__Link_Gen__67F75EFD6F9999EA");

                entity.ToTable("Link_Generation_Table");

                entity.Property(e => e.CandidateId).HasColumnName("Candidate_Id");

                entity.Property(e => e.CandidateContactNumber)
                    .HasMaxLength(15)
                    .HasColumnName("Candidate_Contact_Number");

                entity.Property(e => e.CandidateEmail)
                    .HasMaxLength(100)
                    .HasColumnName("Candidate_Email");

                entity.Property(e => e.CandidateName)
                    .HasMaxLength(100)
                    .HasColumnName("Candidate_Name");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LinkExpiration)
                    .HasColumnType("datetime")
                    .HasColumnName("Link_Expiration");

                entity.Property(e => e.LinkStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Link_Status")
                    .IsFixedLength();

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.VerificationCode)
                    .HasMaxLength(6)
                    .HasColumnName("Verification_Code");
            });

            modelBuilder.Entity<RoleMaster>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Role_mas__D80AB4BB89D68D4D");

                entity.ToTable("Role_master");

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasColumnName("Role_Name");
            });

            modelBuilder.Entity<StateMaster>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK__State_Ma__AF9338F7FE783BB3");

                entity.ToTable("State_Master");

                entity.Property(e => e.StateId).HasColumnName("State_Id");

                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .HasMaxLength(200)
                    .HasColumnName("State_Name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.StateMasters)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__State_Mas__Count__6754599E");
            });

            modelBuilder.Entity<UserMaster>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__User_Mas__206D917071404904");

                entity.ToTable("User_Master");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .HasColumnName("User_Name");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserMasters)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User_Mast__Role___5EBF139D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
