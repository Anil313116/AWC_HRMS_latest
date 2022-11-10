
using AWC_HRMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Net.Mail;
using static AWC_HRMS.Models.LinkGenerationTable;

namespace AWC_HRMS_Web.Controllers
{

    [Authorize]
    
    public class Dashboard : Controller
    {
        // GET: Dashboard
        [AllowAnonymous]
        public IActionResult Index()
        {

            string Bretval0 = this.CandidateNotStarted();
            ViewBag.Vals0 = Bretval0;
            string Bretval1 = this.CandidateHalfDone();
            ViewBag.Vals1 = Bretval1;
            string Bretval2 = this.CandidateCompleted();
            ViewBag.Vals2 = Bretval2;
            string Bretval3 = this.ReinitiateLink();
            ViewBag.Vals3 = Bretval3;

            return View();
        }

        // GET: Dashboard/Details/5

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
                check.Parameters.AddWithValue("@candidateID", 120);

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
                                PassBookCancelChequePri = (byte[])sdr["PassBookCheque_Pri"],
                                PassBookCancelChequeSec = (byte[])sdr["PassBookCheque_Sec"]
                            });

                        }

                    }

                    con.Close();

                }

            }

            return files;

        }





        public JsonResult StatusApprove(linkUpdate linkgeneration)
        {
            ViewData["btn"] = "Save";
           // int a = CandidateId;
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
                        conCommand.Parameters.AddWithValue("@CandidateId", linkgeneration.CandidateId);

                        int i = conCommand.ExecuteNonQuery();
                        if (i > 0)
                        {
                            sendmailStatus(linkgeneration.email, linkgeneration.Name, linkgeneration.Reason,linkgeneration.btn);
                            ViewData["Message"] = "Status is updated and an email is sent to the candidate";


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
            return Json(linkgeneration);
        }


        public JsonResult DeclineStatus(int CandidateId)
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



         
        public JsonResult CandidateStatusDecline(linkUpdate linkgeneration)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");



            if (linkgeneration == null)
            {
                return Json("NotFound");
            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    //var id = HttpContext.Session.GetString("CUser");
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_DeclineCandidateSatus", con);
                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@Candidate_ID", linkgeneration.CandidateId);
                    check.Parameters.AddWithValue("@Candidate_name", linkgeneration.Name);
                    check.Parameters.AddWithValue("@Candidate_Email", linkgeneration.email);
                    check.Parameters.AddWithValue("@Reason", linkgeneration.Reason);
                    check.Parameters.AddWithValue("@Candidate_Contact_Number", linkgeneration.CandidateContactNumber);

                    using (var reader = check.ExecuteReader())
                    {
                        sendmailStatus(linkgeneration.email, linkgeneration.Name, linkgeneration.Reason,linkgeneration.btn);
                        ViewData["Message"] = "Status is updated and an email is sent to the candidate";

                    }



                }



            }
            return Json(linkgeneration);
        }




        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static List<char> characters = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                                                        'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                                                        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                        'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'
        };

        public string linkformation(int CId, string CEmail, string CName, String CContactNumber)
        {



            string maillink;
            maillink = "id=" + CId
                + "&name=" + CName
                + "&Email=" + CEmail
                + "&ContactNumber=" + CContactNumber;



            return maillink;
            //return "'?id=44&name=Surya&email=surya.singh@awcsoftware.com&mobnum=9870876543&linkstatus=0' ";
        }
        public void sendmailStatus(string CEmail, string CName, string Reason,string btn)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


            string br = "";
            br += "<br>";


            string body = "";


            if(btn== "Decline")
            {
                body += "<p>" + "Hello" + " " + CName + ", <br><br>  " + "Greetings from AWC Software! " + br + br + "This is auto generated email.  Your filled details are not correct. please fill your details correctly. " + br +
                        "<u>Reason/s for Declining Application</u>" + br + br + Reason;

            }
            else
            {
                body += "<p>" + "Hello" + " " + CName + ", <br><br>  " + "Greetings from AWC Software! " + br + br + "This is auto generated email. Your filled details are correct. Your application is <b>approved</b>";

            }

            //body += "<p>" + "Hello" + " " + CName + ", <br><br>  " + "Greetings from AWC Software! " + br + br + "This is auto generated email. Please Decline Your details  to begin your on-boarding process. " + br + br + ""                 + "</p><p>Illustration:" + br + br + " Name: Sujit Sharma " + br + "Mail id: sujit.sharma96 @xyz.com " + br + "Phone #: 9876543210 " + br + "" + br + "Then password will be: 7654SUJI3210" + br + " </p><p>Thank you and wishing you a great career with AWC Software!</p>";


            MailAddress from = new MailAddress("akashchoudhary199404@gmail.com");
            MailAddress to = new MailAddress(CEmail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Check Application Status";
            message.IsBodyHtml = true;
            message.Body = body;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("akashchoudhary199404@gmail.com", "ogqhffxlkwmmwiyv"),
                EnableSsl = true
            };



            try
            {
                client.Send(message);
                Console.WriteLine("Mail Sent");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
        [Authorize]
        [NonAction]
        public string CandidateCompleted()
        {
            var retval2 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_CanCompleted", con);
                check.CommandType = CommandType.StoredProcedure;

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {
                            retval2 = reader.GetName(0) + "|" + reader.GetName(1) + "#";
                            
                            while (reader.Read())
                                retval2 = retval2 + reader.GetInt32(0) + "|" + reader.GetString(1)+ "|" + reader.GetString(2) + "|" + reader.GetString(3) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());

                        reader.Close();


                    }

                }

            }
            return retval2;
        }

        [Authorize]
        [NonAction]
        public string CandidateHalfDone()
        {
            var retval1 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_CanHalfDone", con);
                check.CommandType = CommandType.StoredProcedure;


                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {
                            retval1 = reader.GetName(0) + "|" + reader.GetName(1) + "#";
                            Console.WriteLine("\t{0}\t{1}", reader.GetName(0), reader.GetName(1));
                            while (reader.Read())
                                retval1 = retval1 + reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());

                        reader.Close();


                    }

                }

            }
            return retval1;
        }

        [Authorize]
        [NonAction]
        public string CandidateNotStarted()
        {
            var retval0 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_CanNotStarted", con);
                check.CommandType = CommandType.StoredProcedure;


                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {
                            retval0 = reader.GetName(0) + "|" + reader.GetName(1) + "#";
                            while (reader.Read())
                                retval0 = retval0 + reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());

                        reader.Close();


                    }

                }

            }
            return retval0;
        }

        [Authorize]
        [NonAction]
        public string ReinitiateLink()
        {
            var retval3 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_ReinitiateLink", con);
                check.CommandType = CommandType.StoredProcedure;

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {
                            retval3 = reader.GetName(0) + "|" + reader.GetName(1) + "#";

                            while (reader.Read())
                                retval3 = retval3 + reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());

                        reader.Close();


                    }

                }

            }
            return retval3;
        }
        }




}