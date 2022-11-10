using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AWC_HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AWC_HRMS_Web.Controllers
{
    
    public class CandidateRegistrationController : Controller
    {
        // string connection = "data source= DESKTOP-B6EBP3L; initial catalog= AWC_HRMS; integrated security=True";

        [NonAction]
        public string FetchCandidateEducation(int canid)
        {
            var retval3 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchEducationDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        
                        while (reader.Read())
                        if (reader.GetString(1) != null)
                        {
                            //Console.WriteLine(reader.GetString(1));
                            retval3 = reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" + 
                                reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetStream(6) + "|" + 
                                reader.GetStream(7) + "|" + reader.GetString(8) + "|" + reader.GetString(9) + "|" +
                                reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetString(12) + "|" +
                                reader.GetStream(14) + "|" + reader.GetString(15) + "|" + reader.GetString(16) + "|" +
                                reader.GetString(17) + "|" + reader.GetString(18) + "|" + reader.GetString(19) + "|" +
                                reader.GetStream(20) + "|" + reader.GetStream(21) + "|" + reader.GetString(22) + "|" +
                                reader.GetString(23) + "|" + reader.GetString(24) + "|" + reader.GetString(25) + "|" +
                                reader.GetString(26) + "|" + reader.GetStream(27) + "|" + reader.GetStream(28) + "|" + reader.GetString(29) + "|" + reader.GetString(30);
                        }

                        reader.NextResult();

                        //Taking up the current status
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                                if (reader.GetInt32(0) != null)
                                {
                                    //Console.WriteLine(reader.GetString(1));
                                    retval3 = retval3 + '$' + reader.GetInt32(0);
                                }

                        }

                        reader.Close();
                        con.Close();

                    }
                    return retval3;
                }
            }
        }
        [NonAction]
        public string FetchCandidateDetails(int canid)
        {
            var retval2 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchCandidateDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        
                        while (reader.Read())
                          if (reader.GetString(1) != null)
                          {
                                    //Console.WriteLine(reader.GetString(1));
                              retval2 = reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|"
                                   + reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|"
                                   + reader.GetString(7) + "|" + reader.GetDateTime(8) + "|" + reader.GetString(9) + "|"
                                   + reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetInt32(12) + "|"
                                   + reader.GetInt32(13) + "|" + reader.GetString(14) + "|" + reader.GetString(15) + "|"
                                   + reader.GetInt32(16) + "|" + reader.GetInt32(17) + "|" + reader.GetString(18) + "|"
                                   + reader.GetString(19) + "|" + reader.GetString(20) + "|" + reader.GetString(21) + "|" + "|" + reader.GetString(22) + "|"
                                   + reader.GetString(23) + "|" + reader.GetString(24) + "|" + "25" + "|"
                                   + "26" + "|" + "27" + "|" + "28" + "|" + "29" + "|"
                                   + reader.GetString(44) + "|" + reader.GetDateTime(45) + "|" + reader.GetDateTime(46);
                          }
                        
                        reader.NextResult();

                        //Taking up the current status
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                                if (reader.GetInt32(0) != null)
                                {
                                    //Console.WriteLine(reader.GetString(1));
                                    retval2 = retval2 + '$' + reader.GetInt32(0);
                                }

                        }

                        reader.Close();
                        con.Close();

                    }
                    return retval2;
                }
            }
        }

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment;

        public CandidateRegistrationController(Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        public IActionResult CandidateRegisterationDetail(int id)
        {
            int candid = 0;

            if (TempData["LinkStatus"] != null && TempData["LinkStatus"] != null)
            { 
                var linkstatus = TempData["LinkStatus"];
                var urlnames = TempData["NewData"];
             
            
                //TempData.Keep("NewData");
                //TempData.Keep("LinkStatus");

                if (urlnames != null)
                {
                    var qstrvals = urlnames.ToString();
                    var sptstr = qstrvals.Split('&');
                    int i;

                    for (i = 0; i < 1; i++)
                    {
                        var StrVal = sptstr[i].Split('=');
                        ViewBag.NewCId = StrVal[1];
                        candid = Int32.Parse(StrVal[1]);
                    }
                
                    if (linkstatus.ToString() == "0")
                    {
                         ViewBag.myst = linkstatus;
                         ViewBag.NewData = urlnames;
                    }
                    else
                    {
                        string Bretval2 = this.FetchCandidateDetails(candid);
                        if (Bretval2 != "")
                        {
                            var lstatus = Bretval2.Split('$');
                            ViewBag.Vals2 = lstatus[0];
                            ViewBag.myst = lstatus[1];
                            ViewBag.NewCId = candid;
                        }
                    }

                }
                else
                {
                    ViewData["reason"] = "URL Supplied not complete";
                    return View("DatabaseError");
                }
            }
            else {
                string Bretval2 = this.FetchCandidateDetails(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.Vals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = candid;
                }
                else {
                    ViewData["reason"] = "Candidate Not Fetched";
                    return View("DatabaseError");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetail(CandidatePersonalDetails candidate, IList<IFormFile> CandidateImage, int canid)
        {
            ViewData["btn"] = "Save";
            int a = canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            IFormFile uploadedImage = CandidateImage.FirstOrDefault();
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate.CandidateImage = b;
                }

            }
            
            
            using (SqlConnection con = new SqlConnection(constring))
            {

                if (ViewData["btn"] == "Save")
                // if (Convert.ToInt32(candidate.CandidateId) > 0)
                {

                    candidate.CreatedBy = "Admin";
                    con.Open();
                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@ActionType", "Insert");
                    conCommand.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@candidatename", candidate.CandidateName);
                    conCommand.Parameters.AddWithValue("@gender", candidate.Gender);
                    conCommand.Parameters.AddWithValue("@marital_status", candidate.MaritalStatus);
                    conCommand.Parameters.AddWithValue("@dob", candidate.Dob);
                    conCommand.Parameters.AddWithValue("@candidatecontactnumber", candidate.CandidateContactNumber);
                    conCommand.Parameters.AddWithValue("@candidatealternatecontactnumber", candidate.CandidateAlternateNumber);
                    conCommand.Parameters.AddWithValue("@fathername", candidate.FatherName);
                    conCommand.Parameters.AddWithValue("@candidate_emailid", candidate.CandidateEmailId);
                    conCommand.Parameters.AddWithValue("@candidate_image", candidate.CandidateImage);
                    conCommand.Parameters.AddWithValue("@anniversarydate", candidate.AnniversaryDate);
                    //conCommand.Parameters.AddWithValue("@myvar", "");
                    conCommand.Parameters.AddWithValue("@Createdby", "Admin");
                
                    var reader = conCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            ViewBag.myst = reader.GetInt32(0);
                            //ViewBag.myst = reader.GetString(0);
                            ViewBag.NewCId = candidate.CandidateId;

                        }
                        ViewData["reason"] = "Data has been inserted";
                        
                        return View();

                    }

                }
                else
                {

                    con.Open();
                    candidate.CreatedBy = "Admin";
                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@ActionType", "Update");
                    conCommand.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@candidatename", candidate.CandidateName);
                    conCommand.Parameters.AddWithValue("@gender", candidate.Gender);
                    conCommand.Parameters.AddWithValue("@marital_status", candidate.MaritalStatus);
                    conCommand.Parameters.AddWithValue("@dob", candidate.Dob);
                    conCommand.Parameters.AddWithValue("@candidatecontactnumber", candidate.CandidateContactNumber);
                    conCommand.Parameters.AddWithValue("@candidatealternatecontactnumber", candidate.CandidateAlternateNumber);

                    conCommand.Parameters.AddWithValue("@fathername", candidate.FatherName);
                    conCommand.Parameters.AddWithValue("@candidate_emailid", candidate.CandidateEmailId);
                    conCommand.Parameters.AddWithValue("@candidate_image", candidate.CandidateImage);
                   // conCommand.Parameters.AddWithValue("@myvar", "");
                    SqlDataAdapter da = new SqlDataAdapter(conCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();
                }
            }
            
            ViewData["reason"] = "Data has been inserted";
            ViewBag.NewCId = canid;
            //ViewBag.myst = canid;
            return View();

        }


        public IActionResult CandidateRegisterationDetailNext(int id)
        {
            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {
                string Bretval2 = this.FetchCandidateDetails(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.NextVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }
                //ViewBag.Vals2 = Bretval2;
                //ViewBag.NewCId = id;
                //ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetailNext(CandidatePersonalDetails candidate, IList<IFormFile> AadharImage, IList<IFormFile> PanCardImage, IList<IFormFile> PassportImage, int canid)
        {
            IFormFile uploadedImage = AadharImage.FirstOrDefault();
            if (uploadedImage == null)
            {
                byte[] b;
                b= new byte[0];
                candidate.AadharImage = b;
            }

          else if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate.AadharImage = b;
                }
            }

            IFormFile uploadedImage1 = PanCardImage.FirstOrDefault();

            if (uploadedImage1 == null)
            {
                byte[] b;
                b = new byte[0];
                candidate.PanCardImage = b;

            }
            else if (uploadedImage1.ContentType.ToLower().StartsWith("image/"))
            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage1.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage1.OpenReadStream().Length);
                    candidate.PanCardImage = b;
                }
            }

            IFormFile uploadedImage2 = PassportImage.FirstOrDefault();
            if (uploadedImage2 == null)
            {
                byte[] b;
                b = new byte[0];
                candidate.PassportImage = b;
            }
            else if (uploadedImage2.ContentType.ToLower().StartsWith("image/"))
            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage2.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage2.OpenReadStream().Length);
                    candidate.PassportImage = b;
                }
            }


            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");



            using (SqlConnection con = new SqlConnection(constring))
            {

                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_updatecandidatedetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", candidate.CandidateId);
                check.Parameters.AddWithValue("@EmergencyContactName", candidate.EmergencyContactName);
                check.Parameters.AddWithValue("@EmergencyContactNo", candidate.EmergencyContactNo);
                check.Parameters.AddWithValue("@EmergencyContactRelation", candidate.EmergencyContactRelation);
                check.Parameters.AddWithValue("@AadharNumber", candidate.AadharNumber);
                check.Parameters.AddWithValue("@AadharImage", candidate.AadharImage);
                check.Parameters.AddWithValue("@PanNumber", candidate.PanNumber);
                check.Parameters.AddWithValue("@PanImage", candidate.PanCardImage);
                check.Parameters.AddWithValue("@Passport_Number", candidate.PassportNumber);
                check.Parameters.AddWithValue("@PassportImage", candidate.PassportImage);
                check.Parameters.AddWithValue("@UANNumber", candidate.UANNumber);
                check.Parameters.AddWithValue("@LastpfDate", candidate.LastpfDate);

                var reader = check.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        ViewBag.myst = reader.GetInt32(0);
                        //ViewBag.myst = reader.GetString(0);
                        ViewBag.NewCId = candidate.CandidateId;

                    }
                    ViewData["reason"] = "Data has been inserted";
                }
            }
            //        check.ExecuteNonQuery();

            //    con.Close();

            //}
            //ViewData["reason"] = "Data has been inserted";
            ////ViewBag.myst = candidate.CandidateId;
            //ViewBag.myst = candidate.LinkStatus;
            //ViewBag.NewCId = candidate.CandidateId;
            return View();
        }



        public IActionResult CandidateRegisterationDetailAddress(int id)
        {

            CandidatePersonalDetails emp = new CandidatePersonalDetails();
            emp.stateMasters = State();
            
            //StateId = 27;
            emp.cityMasters = City(0);
            if (id == 0)
            {
                return View(emp);
                //return View();
            }
            else
            {
                string Bretval2 = this.FetchCandidateDetails(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.AddVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }
                return View(emp);
                //return View();
            }
        }
      
        [HttpPost]
        public IActionResult CandidateRegisterationDetailAddress(CandidatePersonalDetails candidate, IFormFile file, int canid)
        {
           
            ViewData["btn"] = "Save";
            int a = canid;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
          
            using (SqlConnection con = new SqlConnection(constring))
            {

                if (ViewData["btn"] == "Save")
                {
                    try
                    {
                        if(candidate.PermanentStateId==99)
                        {
                            candidate.PermanentCityId = 9999;
                        }

                        if (candidate.CurrentStateId == 99)
                        {
                            candidate.CurrentCityId = 9999;
                        }
                        
                        con.Open();
                        SqlCommand conCommand = new SqlCommand("UspCandidateAddressDetails", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        //conCommand.Parameters.AddWithValue("@ActionType", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@Permanent_Address", candidate.PermanentAddress);
                        conCommand.Parameters.AddWithValue("@Permanent_State_ID", candidate.PermanentStateId);
                        conCommand.Parameters.AddWithValue("@Permanent_City_ID", candidate.PermanentCityId);
                        conCommand.Parameters.AddWithValue("@Permanent_State_other", candidate.PermanentOtherState);
                        conCommand.Parameters.AddWithValue("@Permanent_City_other", candidate.PermanentOtherCity);

                        conCommand.Parameters.AddWithValue("@Permanent_PinCode", candidate.PermanentPinCode);
                        conCommand.Parameters.AddWithValue("@Current_Address", candidate.CurrentAddress);
                        conCommand.Parameters.AddWithValue("@Current_State_ID", candidate.CurrentStateId);
                        conCommand.Parameters.AddWithValue("@Current_City_ID", candidate.CurrentCityId);
                        conCommand.Parameters.AddWithValue("@Current_State_other", candidate.CurrentOtherState);
                        conCommand.Parameters.AddWithValue("@Current_City_other", candidate.CurrentOtherCity);

                        conCommand.Parameters.AddWithValue("@Current_PinCode", candidate.CurrentPinCode);
                        var reader = conCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                ViewBag.myst = reader.GetInt32(0);
                                //ViewBag.myst = reader.GetString(0);
                                ViewBag.NewCId = candidate.CandidateId;

                            }
                        }
                         //   conCommand.ExecuteNonQuery();
                        con.Close();
                        
                    }

                    catch (Exception ex)
                    {
                        return View("DatabaseError");
                        //ex.Message.ToString();
                    }
                }
                else
                {


                }
            }

            ViewData["reason"] = "Data has been inserted";
            
            CandidatePersonalDetails emp = new CandidatePersonalDetails();
            emp.stateMasters = State();
          
            return View(emp);
         
        }

        public IActionResult CandidateRegisterationDetailQualification(int id)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {
                string Bretval2 = this.FetchCandidateEducation(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.PGVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }
                else
                {
                    string queryString = "select Link_Status from Link_Generation_Table where Candidate_Id='" + id + "'";
                    using (SqlConnection upcon = new SqlConnection(constring))
                    {
                        SqlCommand upcheck = new SqlCommand(queryString, upcon);
                        upcon.Open();
                        var reader = upcheck.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                                if (reader.GetInt32(0) != null)
                                {
                                    ViewBag.myst = reader.GetInt32(0);
                                    ViewBag.NewCId = id;
                                }
                        }
                        upcon.Close();
                    }

                }
                //ViewBag.Vals2 = Bretval2;
                //ViewBag.NewCId = id;
                //ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetailQualification(CandidateEducation candidate, IList<IFormFile> PgMarkSheet, IList<IFormFile> PgDegreePassingCertificate, int canid)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            IFormFile uploadedImage = PgMarkSheet.FirstOrDefault();
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate.PgMarkSheet = b;
                }

            }

            IFormFile uploadedImage1 = PgDegreePassingCertificate.FirstOrDefault();
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage1.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage1.OpenReadStream().Length);
                    candidate.PgDegreePassingCertificate = b;
                }

            }
            

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;

                if (ViewData["btn"] == "Save")
         
                {

                    try
                    {
                        con.Open();

                        SqlCommand conCommand = new SqlCommand("UspPGEducation", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Insert");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@PG_College_Name", candidate.PgCollegeName);
                        conCommand.Parameters.AddWithValue("@PG_University", candidate.PgUniversity);
                        conCommand.Parameters.AddWithValue("@PG_Course", candidate.PgStream);
                        conCommand.Parameters.AddWithValue("@PG_Year_of_Passing", candidate.PgYearOfPassing);
                        conCommand.Parameters.AddWithValue("@PG_PercentageCGPA", candidate.PgPercentageCgpa);
                        conCommand.Parameters.AddWithValue("@PG_Degree", candidate.PG_Degree_Name);
                        conCommand.Parameters.AddWithValue("@PG_MarkSheet", candidate.PgMarkSheet);
                        conCommand.Parameters.AddWithValue("@PG_DegreePassing_Certificate", candidate.PgDegreePassingCertificate);
                        var reader = conCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                ViewBag.myst = reader.GetInt32(0);
                                //ViewBag.myst = reader.GetString(0);
                                ViewBag.NewCId = candidate.CandidateId;

                            }
                        }
                        //conCommand.ExecuteNonQuery();

                        con.Close();
                       
                        ViewData["reason"] = "Data has been inserted";
                    }

                    catch (Exception ex)
                    {
                        return View("DatabaseError");
                        //ex.Message.ToString();
                    }

                }
                else
                {

                    con.Open();

                    SqlCommand conCommand = new SqlCommand("Usp_SaveCandidateDetails", con);
                    conCommand.CommandType = CommandType.StoredProcedure;
                    conCommand.Parameters.AddWithValue("@Action", "Update");
             
                    conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                    conCommand.Parameters.AddWithValue("@PG_College_Name", candidate.PgCollegeName);
                    conCommand.Parameters.AddWithValue("@PG_University", candidate.PgUniversity);
                    conCommand.Parameters.AddWithValue("@PG_Course", candidate.PgCourse);
                    conCommand.Parameters.AddWithValue("@PG_Year_of_Passing", candidate.PgYearOfPassing);
                    conCommand.Parameters.AddWithValue("@PG_PercentageCGPA", candidate.PgPercentageCgpa);
                    //conCommand.Parameters.AddWithValue("@PG_MarkSheet", candidate.PgMarkSheet);
                    //conCommand.Parameters.AddWithValue("@PG_DegreePassing_Certificate", candidate.PgDegreePassingCertificate);
                    //conCommand.Parameters.AddWithValue("@candidate_image", FN);
                    SqlDataAdapter da = new SqlDataAdapter(conCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    con.Close();

                    
                }
            }
            ViewData["reason"] = "Data has been inserted";
            //ViewBag.myst = reader.GetInt32(0);
            //ViewBag.myst = reader.GetString(0);
            ViewBag.NewCId = candidate.CandidateId;

            //ViewBag.myst = candidate.CandidateId;
            //ViewBag.myst = candidate.LinkStatus;
            //ViewBag.NewCId = candidate.CandidateId;
            return View();
        }

        public IActionResult CandidateRegisterationDetail_QualificationUG(int id)
        
        {
            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {

                string Bretval2 = this.FetchCandidateEducation(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.UGVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }
                //ViewBag.Vals2 = Bretval2;
                //ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }
        [HttpPost]
        public IActionResult CandidateRegisterationDetail_QualificationUG(CandidateEducation candidate, IList<IFormFile> UGMarkSheet, IList<IFormFile> UgDegreePassingCertificate, int canid)
        {
            ViewData["btn"] = "Save";
            int a=  canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            IFormFile uploadedImage = UGMarkSheet.FirstOrDefault();
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate.UgMarkSheet = b;
                }

            }

            IFormFile uploadedImage1 = UgDegreePassingCertificate.FirstOrDefault();
            if (uploadedImage1.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage1.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage1.OpenReadStream().Length);
                    candidate.UgDegreePassingCertificate = b;
                }

            }

            using (SqlConnection con = new SqlConnection(constring))
            {

                if (ViewData["btn"] == "Save")
               
                {
                    try
                    {
                        con.Open();
                         SqlCommand conCommand = new SqlCommand("UspUGEducation", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@UG_College_Name", candidate.UgCollegeName);
                        conCommand.Parameters.AddWithValue("@UG_University", candidate.UgUniversity);
                        conCommand.Parameters.AddWithValue("@UG_Course", candidate.UgStream);
                        conCommand.Parameters.AddWithValue("@UG_Year_of_Passing", candidate.UgYearOfPassing);
                        conCommand.Parameters.AddWithValue("@UG_PercentageCGPA", candidate.UgPercentageCgpa);
                        conCommand.Parameters.AddWithValue("@UG_Degree", candidate.UG_Degree_Name);
                        conCommand.Parameters.AddWithValue("@UG_MarkSheet", candidate.UgMarkSheet);
                        conCommand.Parameters.AddWithValue("@UG_DegreePassing_Certificate", candidate.UgDegreePassingCertificate);
                        var reader = conCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                ViewBag.myst = reader.GetInt32(0);
                                //ViewBag.myst = reader.GetString(0);
                                ViewBag.NewCId = candidate.CandidateId;

                            }
                        }
                        //conCommand.ExecuteNonQuery();
                        con.Close();
                    }

                    catch (Exception ex)
                    {
                         return View("DatabaseError");
                        //ex.Message.ToString();
                    }
                }
                else
                {

                }
            }

            ViewData["reason"] = "Data has been inserted";
            //ViewBag.myst = candidate.CandidateId;
            //ViewBag.myst = candidate.LinkStatus;
            ViewBag.NewCId = candidate.CandidateId;
            return View();
        }

        public IActionResult CandidateRegisterationDetail_Qualification12(int id)
        {
            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {
                
                string Bretval2 = this.FetchCandidateEducation(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.TwelfthVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }

                //ViewBag.Vals2 = Bretval2;
                //ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }

        [HttpPost]
        public IActionResult CandidateRegisterationDetail_Qualification12(CandidateEducation candidate, IList<IFormFile> _12MarkSheet, IList<IFormFile> _12DegreePassingCertificate, int canid)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            IFormFile uploadedImage = _12MarkSheet.FirstOrDefault();
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate._12MarkSheet = b;
                }

            }

            IFormFile uploadedImage1 = _12DegreePassingCertificate.FirstOrDefault();
            if (uploadedImage1.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage1.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage1.OpenReadStream().Length);
                    candidate._12DegreePassingCertificate = b;
                }

            }

            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {
                    int a = canid;

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("Usp12Education", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@12_Schoole_Name", candidate._12SchoolName);
                        conCommand.Parameters.AddWithValue("@12_Board_Name", candidate._12BoardName);
                        conCommand.Parameters.AddWithValue("@12_Course", candidate._12Stream);
                        conCommand.Parameters.AddWithValue("@12_Year_of_Passing", candidate._12YearOfPassing);
                        conCommand.Parameters.AddWithValue("@12_PercentageCGPA", candidate._12PercentageCgpa);
                        conCommand.Parameters.AddWithValue("@12_MarkSheet", candidate._12MarkSheet);
                        conCommand.Parameters.AddWithValue("@12_DegreePassing_Certificate", candidate._12DegreePassingCertificate);

                        var reader = conCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                ViewBag.myst = reader.GetInt32(0);
                                //ViewBag.myst = reader.GetString(0);
                                ViewBag.NewCId = candidate.CandidateId;

                            }
                        }
                        //conCommand.ExecuteNonQuery();
                        con.Close();
                        
                    }
                    else
                    {


                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

            }
            ViewData["reason"] = "Data has been inserted";
            //ViewBag.myst = candidate.CandidateId;
            //ViewBag.myst = candidate.LinkStatus;
            ViewBag.NewCId = candidate.CandidateId;
            return View();
        }
        public IActionResult CandidateRegisterationDetail_Qualification10(int id)
        {
            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {
                string Bretval2 = this.FetchCandidateEducation(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.TenthVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }
                //ViewBag.Vals2 = Bretval2;
                //ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }

        }
        [HttpPost]
        public IActionResult CandidateRegisterationDetail_Qualification10(CandidateEducation candidate, IList<IFormFile> _10MarkSheet, IList<IFormFile> _10DegreePassingCertificate, int canid)
        {
            ViewData["btn"] = "Save";
            int a = canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            IFormFile uploadedImage = _10MarkSheet.FirstOrDefault();
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate._10MarkSheet = b;
                }

            }

            IFormFile uploadedImage1 = _10DegreePassingCertificate.FirstOrDefault();
            if (uploadedImage1.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage1.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage1.OpenReadStream().Length);
                    candidate._10DegreePassingCertificate = b;
                }

            }


            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("Usp10Education", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@10_Schoole_Name", candidate._10SchoolName);
                        conCommand.Parameters.AddWithValue("@10_Board_Name", candidate._10BoardName);
                        conCommand.Parameters.AddWithValue("@10_Course", candidate._10Stream);
                        conCommand.Parameters.AddWithValue("@10_Year_of_Passing", candidate._10YearOfPassing);
                        conCommand.Parameters.AddWithValue("@10_PercentageCGPA", candidate._10PercentageCgpa);
                        conCommand.Parameters.AddWithValue("@10_MarkSheet", candidate._10MarkSheet);
                        conCommand.Parameters.AddWithValue("@10_DegreePassing_Certificate", candidate._10DegreePassingCertificate);
                        var reader = conCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                ViewBag.myst = reader.GetInt32(0);
                                //ViewBag.myst = reader.GetString(0);
                                ViewBag.NewCId = candidate.CandidateId;

                            }
                        }
                        //conCommand.ExecuteNonQuery();
                        con.Close();
                      
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

            }
            ViewData["reason"] = "Data has been inserted";
            ViewBag.NewCId = candidate.CandidateId;
            //ViewBag.myst = candidate.CandidateId;
            //ViewBag.myst = candidate.LinkStatus;
            //ViewBag.NewCId = candidate.CandidateId;
            return View();
        }

        public IActionResult CandidateRegisterationDetail_Employement(int id)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {
                
                CandidateEmployement employement = new CandidateEmployement();
                string Bretval2 = this.FetchEmployeeDetails(id);
                if (Bretval2 != "")
                {

                    var lstatus = Bretval2.Split('$');
                    //ViewBag.TenthVals2 = lstatus[0];
                    //ViewBag.myst = lstatus[1];
                    //ViewBag.NewCId = id;

                    ViewBag.EmpVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                    
                    //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });
                }
                else {
                    string queryString = "select Link_Status from Link_Generation_Table where Candidate_Id='" + id + "'";
                    using (SqlConnection upcon = new SqlConnection(constring))
                    {
                        SqlCommand upcheck = new SqlCommand(queryString, upcon);
                        upcon.Open();
                        var reader = upcheck.ExecuteReader();
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                                if (reader.GetInt32(0) != null)
                                {
                                    ViewBag.myst = reader.GetInt32(0);
                                    ViewBag.NewCId = id;
                                }
                        }
                        upcon.Close();
                    }
                }
                return View(employement);
            }
        }
        
        [NonAction]
        public string FetchBankDetails(int canid)
        {
            var retval2 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                //int a = canid;
                int a = 116;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchBankDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())

                            if (reader.GetString(1) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval2 = reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|"
                                    + reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(7) + "|"
                                    + reader.GetString(8) + "|" + reader.GetString(9) + "|" + reader.GetString(10) + "|"
                                    + reader.GetString(11);
                            }

                        reader.NextResult();

                        //Taking up the current status
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                                if (reader.GetInt32(0) != null)
                                {
                                    //Console.WriteLine(reader.GetString(1));
                                    retval2 = retval2 + '$' + reader.GetInt32(0);
                                }
                        }

                        reader.Close();
                        con.Close();

                    }
                    return retval2;
                }
            }
        }
        [NonAction]
        public string FetchEmployeeDetails(int canid)
        {
            var retval2 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = canid;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchEmploymentDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        
                        if (reader.GetString(1) != null)
                        {
                            //Console.WriteLine(reader.GetString(1));
                            retval2 = reader.GetString(1) + "|" + reader.GetInt32(2) + "|" + reader.GetInt32(3) + "|"
                                + reader.GetInt32(4);
                        }
                        
                        reader.NextResult();

                        //Taking up the current status
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                                if (reader.GetInt32(0) != null)
                                {
                                    //Console.WriteLine(reader.GetString(1));
                                    retval2 = retval2 + '$' + reader.GetInt32(0);
                                }
                        }

                        reader.Close();
                        con.Close();
                        
                    }
                    return retval2;
                }
            }
        }



        [HttpPost]
        public IActionResult CandidateRegisterationDetail_Employement(CandidateEmployement candidate, IFormFile file, int canid)
        {
            ViewData["btn"] = "Save";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");



            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {

                        if (candidate.FresherExperienced == "Fresher")
                        {
                            candidate.TotalExperiencedYear = 0;
                            candidate.TotalExperiencedMonth = 0;
                            candidate.NoOfCompany = 0;
                        }


                        //candidate.CandidateId = 1;
                        con.Open();
                        SqlCommand conCommand = new SqlCommand("UspEmployeement", con);
                        conCommand.CommandType = CommandType.StoredProcedure;

                        conCommand.Parameters.AddWithValue("@Candidate_Id", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@FresherExperienced", candidate.FresherExperienced);
                        conCommand.Parameters.AddWithValue("@Total_Experienced_Year", candidate.TotalExperiencedYear);
                        conCommand.Parameters.AddWithValue("@Total_Experienced_Month", candidate.TotalExperiencedMonth);
                        conCommand.Parameters.AddWithValue("@No_of_Company", candidate.NoOfCompany);



                        conCommand.ExecuteNonQuery();
                        con.Close();
                        //InsertCustomers(customers);

                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    return View("DatabaseError");
                }



            }

            ViewData["reason"] = "Employement Details inserted successfully!!";
            ViewBag.myst = candidate.CandidateId;
            ViewBag.NewCId = candidate.CandidateId;

            return View();
        }





        ///////New Code for Bank Details form
        public IActionResult CandidateBankDetails(int id)
        {
            id = 120;
            ViewBag.BankName = "ICICI Bank Ltd.";
            if (id == 0)
            {
                return View("DatabaseError");
            }
            else
            {
                string Bretval2 = this.FetchBankDetails(id);
                if (Bretval2 != "")
                {
                    var lstatus = Bretval2.Split('$');
                    ViewBag.TenthVals2 = lstatus[0];
                    ViewBag.myst = lstatus[1];
                    ViewBag.NewCId = id;
                }
                //ViewBag.Vals2 = Bretval2;
                //ViewBag.myst = id;
                return View();
                //return RedirectToAction("CandidateRegisterationDetail", new { canid = id });

            }
           

        }
        [HttpPost]
        public IActionResult CandidateBankDetails(CandidatePersonalDetails candidate, IList<IFormFile> PassBookCancelChequePri, IList<IFormFile> PassBookCancelChequeSec, int canid)
        {
            ViewData["btn"] = "Save";
            int a = canid;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            IFormFile uploadedImage = PassBookCancelChequePri.FirstOrDefault();
            
            if (uploadedImage.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                    candidate.PassBookCancelChequePri = b;
                }

            }

            IFormFile uploadedImage1 = PassBookCancelChequeSec.FirstOrDefault();
            if (uploadedImage1.ContentType.ToLower().StartsWith("image/"))

            {
                byte[] b;
                using (BinaryReader br = new BinaryReader(uploadedImage1.OpenReadStream()))
                {
                    b = br.ReadBytes((int)uploadedImage1.OpenReadStream().Length);
                    candidate.PassBookCancelChequeSec = b;
                }

            }


            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("UspCandidateBankDetails", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        //conCommand.Parameters.AddWithValue("@Action", "Update");
                        conCommand.Parameters.AddWithValue("@candidateID ", candidate.CandidateId);
                        conCommand.Parameters.AddWithValue("@Bank_Account_Holder_Name_Pri", candidate.BankAccountHolderNamePri);
                        conCommand.Parameters.AddWithValue("@Bank_Name_Pri", candidate.BankNamePri);
                        conCommand.Parameters.AddWithValue("@Bank_Account_Number_Pri", candidate.BankAccountNumberPri);
                        conCommand.Parameters.AddWithValue("@IFSC_Code_Pri", candidate.IfscCodePri);
                        conCommand.Parameters.AddWithValue("@Bank_Branch_Address_Pri", candidate.BankBranchAddressPri);
                        conCommand.Parameters.AddWithValue("@PassBookCancel_Cheque_Pri", candidate.PassBookCancelChequePri);
                        conCommand.Parameters.AddWithValue("@Bank_Account_Holder_Name_Sec", candidate.BankAccountHolderNameSec);

                        conCommand.Parameters.AddWithValue("@Bank_Name_Sec", candidate.BankNameSec);
                        conCommand.Parameters.AddWithValue("@Bank_Account_Number_Sec", candidate.BankAccountNumberSec);
                        conCommand.Parameters.AddWithValue("@IFSC_Code_Sec", candidate.IfscCodeSec);

                        conCommand.Parameters.AddWithValue("@Bank_Branch_Address_Sec", candidate.BankBranchAddressSec);
                        conCommand.Parameters.AddWithValue("@PassBookCancel_Cheque_Sec", candidate.PassBookCancelChequeSec);

                        var reader = conCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                ViewBag.myst = reader.GetInt32(0);
                                //ViewBag.myst = reader.GetString(0);
                                ViewBag.NewCId = candidate.CandidateId;

                            }
                        }
                        con.Close();

                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

            }
            ViewData["reason"] = "Employement Details inserted successfully!!";
            ViewBag.myst = candidate.CandidateId;
            //ViewBag.myst = candidate.LinkStatus;
            //ViewBag.NewCId = candidate.CandidateId;
            return View();
        }

        ////////End of Bank Details form
        ///
        //  End Here Testing

        private static List<StateMaster> State()
        {
            List<StateMaster> Statelist = new List<StateMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UspGetState";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet _dtSet = new DataSet();
                    _sqlDataAdapter.Fill(_dtSet);

                    con.Open();

                    for (int i = 0; i < _dtSet.Tables[0].Rows.Count; i++)
                    {
                        Statelist.Add(new StateMaster
                        {
                            StateId = (int)_dtSet.Tables[0].Rows[i]["State_Id"],
                            StateName = _dtSet.Tables[0].Rows[i]["State_Name"].ToString()
                        });
                    }


                    con.Close();
                }
            }

            return Statelist;
        }

        private static List<CityMaster> City(int StateId)
        {
            //StateId = 27;
            List<CityMaster> Citylist = new List<CityMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UspGetCity";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State_Id", StateId);
                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet _dtSet = new DataSet();
                    _sqlDataAdapter.Fill(_dtSet);

                    con.Open();

                    for (int i = 0; i < _dtSet.Tables[0].Rows.Count; i++)
                    {
                        Citylist.Add(new CityMaster
                        {
                            CityId = (int)_dtSet.Tables[0].Rows[i]["City_Id"],
                            CityName = _dtSet.Tables[0].Rows[i]["City_Name"].ToString()
                        });
                    }

                    con.Close();
                }
            }

            return Citylist;
        }



        [HttpPost]
        public JsonResult GetCity(int StateId)
        {
            List<CityMaster> Citylist = new List<CityMaster>();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UspGetCity";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State_Id", StateId);
                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataSet _dtSet = new DataSet();
                    _sqlDataAdapter.Fill(_dtSet);

                    con.Open();

                    for (int i = 0; i < _dtSet.Tables[0].Rows.Count; i++)
                    {
                        Citylist.Add(new CityMaster
                        {
                            CityId = (int)_dtSet.Tables[0].Rows[i]["City_Id"],
                            CityName = _dtSet.Tables[0].Rows[i]["City_Name"].ToString()
                        });
                    }
                   

                    con.Close();
                }
            }
           

            return Json(Citylist);
        }

        public ActionResult AllCandidateDetails(int id)
        {
            var retval0 = "";
            var retval1 = "";
            var retval2 = "";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchAllCandidateData", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateID", id);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())// Iterate through Candidate Master Table
                            if (reader.GetString(1) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval0 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" +
                                    reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|" +
                                    reader.GetString(7) + "|" + reader.GetDateTime(8) + "|" + reader.GetString(9) + "|" +
                                    reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetInt32(12) + "|" +
                                    reader.GetString(13) + "|" + reader.GetInt32(14) + "|" + reader.GetString(15) + "|" +
                                    reader.GetString(16) + "|" + reader.GetString(17) + "|" + reader.GetInt32(18) + "|" +
                                    reader.GetString(19) + "|" + reader.GetInt32(20) + "|" + reader.GetString(21) + "|" +
                                    reader.GetString(22) + "|" + reader.GetString(23) + "|" + reader.GetString(24) + "|" +
                                    reader.GetString(25) + "|" + reader.GetString(26) + "|" + reader.GetString(27) + "|" +
                                    reader.GetString(28) + "|" + reader.GetStream(29) + "|" + reader.GetStream(30) + "|" +
                                    reader.GetStream(31) + "|" + reader.GetStream(32) + "|" + reader.GetString(33) + "|" + reader.GetString(34)
                                    + "|" + reader.GetString(35) + "|" + reader.GetString(36) + "|" + reader.GetString(37) + "|" + reader.GetStream(38)
                                    + "|" + reader.GetBoolean(39) + "|" + reader.GetString(40) + "|" + reader.GetString(41) + "|" + reader.GetString(42)
                                    + "|" + reader.GetString(43) + "|" + reader.GetString(44) + "|" + reader.GetStream(45) + "|" + reader.GetString(46)
                                    + "|" + reader.GetDateTime(47) + "|" + reader.GetDateTime(48);
                            }

                        reader.NextResult();

                        while (reader.Read())// Iterate through Candidate Education Table
                            if (reader.GetInt32(0) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval1 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" +
                                    reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetStream(6) + "|" +
                                    reader.GetStream(7) + "|" + reader.GetString(8) + "|" + reader.GetString(9) + "|" +
                                    reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetString(12) + "|" +
                                    reader.GetStream(13) + "|" + reader.GetStream(14) + "|" + reader.GetString(15) + "|" +
                                    reader.GetString(16) + "|" + reader.GetString(17) + "|" + reader.GetString(18) + "|" +
                                    reader.GetString(19) + "|" + reader.GetStream(20) + "|" + reader.GetStream(21) + "|" +
                                    reader.GetString(22) + "|" + reader.GetString(23) + "|" + reader.GetString(24) + "|" +
                                    reader.GetString(25) + "|" + reader.GetString(26) + "|" + reader.GetStream(27) + "|" +
                                    reader.GetStream(28);

                            }

                        reader.NextResult();

                        while (reader.Read())// Iterate through Candidate Employement Table
                            if (reader.GetInt32(0) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval2 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetInt32(2) + "|" + reader.GetInt32(3) + "|" + reader.GetInt32(4);

                            }

                    }
                }
            }

            @ViewBag.Vals0 = retval0;
            @ViewBag.Vals1 = retval1;
            @ViewBag.Vals2 = retval2;
            @ViewBag.myst = id;
            return View(PopulateFiles());
        }

        public static List<CandidatePersonalDetails> PopulateFiles()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


            byte[] CandidateImage = null;

            List<CandidatePersonalDetails> files = new List<CandidatePersonalDetails>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand check = new SqlCommand("UspGetImage", con))
                {

                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@candidateID", 120);


                    check.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = check.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new CandidatePersonalDetails
                            {
                                CandidateImage = (byte[])sdr["Candidate_Image"],

                                AadharImage = (byte[])sdr["Aadhar_Image"],
                                PanCardImage = (byte[])sdr["PanCard_Image"],
                                PassportImage = (byte[])sdr["Passport_Image"],
                                Certificate10 = (byte[])sdr["Certificate10"],
                                MarkSheet10 = (byte[])sdr["MarkSheet10"],
                                Certificate12 = (byte[])sdr["Certificate12"],
                                MarkSheet12 = (byte[])sdr["MarkSheet12"],
                                DegreeUG = (byte[])sdr["CertificateUG"],
                                MarkSheetUG = (byte[])sdr["UGMarkSheet"],
                                DegreePG = (byte[])sdr["CertificatePG"],
                                MarkSheetPG = (byte[])sdr["PGMarkSheet"],
                                //PassBookCancelChequePri = (byte[])sdr["PassBookCheque_Pri"],
                                //PassBookCancelChequeSec = (byte[])sdr["PassBookCheque_Sec"]
                            });

                        }

                    }

                    con.Close();

                }

            }

            return files;

        }

        [HttpPost]
        public JsonResult InsertCustomers(List<CandidateEmployement> customers)
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            List<CandidateEmployement> candidateEmployements = new List<CandidateEmployement>(customers);
            var serializeData = candidateEmployements;
            using (var con = new SqlConnection(constring))
            {
               
                foreach (var data in serializeData)
                {
                    if(data.FresherExperienced=="Fresher")
                    {

                        data.NameOfCompany = "0";
                        data.DOJ = "0";
                            data.DOL = "0";
                        data.Designation = "0";
                        data.CTC = "0";
                    }
                    if (data.RelatedDocument == null)
                    {

                        data.RelatedDocument = "NA";
                        
                    }


                    data.CandidateId = 1;
                     //data.RelatedDocument = "Null";
                    //SqlCommand conCommand = new SqlCommand("UspEmployeement", con);
                    //conCommand.CommandType = CommandType.StoredProcedure;
                    using (var cmd = new SqlCommand("UspCompanyExperience", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Candidate_Id", data.CandidateId);
                        cmd.Parameters.AddWithValue("@Company_Name", data.NameOfCompany);
                        cmd.Parameters.AddWithValue("@DOJ", data.DOJ);
                        cmd.Parameters.AddWithValue("@DOL", data.DOL);
                        cmd.Parameters.AddWithValue("@Designation", data.Designation);
                        cmd.Parameters.AddWithValue("@CTC", data.CTC);
                        cmd.Parameters.AddWithValue("@Relieving_Letter", data.RelatedDocument);
                        cmd.Parameters.AddWithValue("@Reason", data.Reason);


                        //cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Connection = con;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return null;


            //using (CustomersEntities entities = new CustomersEntities())
            //{
            //    //Truncate Table to delete all old records.
            //    entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customers]");

            //    //Check for NULL.
            //    if (customers == null)
            //    {
            //        customers = new List<Customer>();
            //    }

            //    //Loop and insert records.
            //    foreach (Customer customer in customers)
            //    {
            //        entities.Customers.Add(customer);
            //    }
            //    int insertedRecords = entities.SaveChanges();
              //return Json(customers);
            //}
        }

        public JsonResult UpdateStatus(int CandidateId)
        {
            ViewData["btn"] = "Save";
            int a = CandidateId;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


            using (SqlConnection con = new SqlConnection(constring))
            {
                try
                {

                    if (ViewData["btn"] == "Save")
                    // if (Convert.ToInt32(candidate.CandidateId) > 0)
                    {


                        con.Open();
                        SqlCommand conCommand = new SqlCommand("Uspupdatelink", con);
                        conCommand.CommandType = CommandType.StoredProcedure;
                        conCommand.Parameters.AddWithValue("@CandidateId", 10);


                        int i = conCommand.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ViewData["reason"] = "Data has been inserted.";
                        }
                        else
                        {
                            ViewData["reason"] = "Data has not been inserted.";
                        }
                        con.Close();

                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

            }


            return Json(CandidateId);
        }
    }
}
