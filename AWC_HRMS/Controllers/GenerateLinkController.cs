using AWC_HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using Microsoft.Net;
using System.Net;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWC_HRMS_Web.Controllers
{
    public class GenerateLinkController : Controller
    {
        public IActionResult GenerateCandidateLink()
        {
            var uid = HttpContext.Session.GetString("CUser");
            if (uid != null)
            {
                return View();
            }
            else
                return RedirectToAction("CheckLogin", "UserLogin");
        }

        string link = GetURL();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenerateCandidateLink(LinkGenerationTable linkgeneration)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            Random rand = new Random();
            string random = rand.Next(0, 10000).ToString("D4");
            string maillink, UpdateUrl;
            string randomdb = random + linkgeneration.CandidateEmail.Substring(0, 4).ToUpper() + linkgeneration.CandidateContactNumber.Substring(linkgeneration.CandidateContactNumber.Length - 4);
            int NewCandidateId = 0;
            int i;

            if (linkgeneration == null)
            {
                return NotFound();
            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    //var id = HttpContext.Session.GetString("CUser");
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_LinkGeneration", con);
                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@Candidate_ID", "");
                    check.Parameters.AddWithValue("@Candidate_name", linkgeneration.CandidateName);
                    check.Parameters.AddWithValue("@Candidate_Email", linkgeneration.CandidateEmail);
                    check.Parameters.AddWithValue("@verifification_code", randomdb);
                    check.Parameters.AddWithValue("@Candidate_Contact_Number", linkgeneration.CandidateContactNumber);
                    check.Parameters.AddWithValue("@Link", link);
                    check.Parameters.AddWithValue("@dateofjoining", linkgeneration.DateOfJoining);
                    check.Parameters.AddWithValue("@Offerdate", linkgeneration.Offer_Date);
                    check.Parameters.AddWithValue("@ExpDate", linkgeneration.LinkExpiration);
                    check.Parameters.AddWithValue("@CreatedBy", "Admin");
                    check.Parameters.AddWithValue("exdays", linkgeneration.Exp_Days);
                    check.Parameters.AddWithValue("@status", "0");

                    //i = check.ExecuteNonQuery();

                    //if (i == -1)
                    //{
                    //    sendmail(linkgeneration.CandidateEmail, linkgeneration.CandidateName, random, link);
                    //    ViewData["Message"] = "Link generated and a email is sent to the candidate";
                    //}//link generation controller

                    using (var reader = check.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            do
                            {
                                while (reader.Read())

                                    NewCandidateId = reader.GetInt32(0);
                                //reader.Close();
                            }
                            while (reader.NextResult());
                            reader.Close();

                            if (NewCandidateId != 0)
                            {
                                maillink = linkformation(NewCandidateId, linkgeneration.CandidateEmail, linkgeneration.CandidateName, linkgeneration.CandidateContactNumber);
                                var updturl = sendmail(linkgeneration.CandidateEmail, linkgeneration.CandidateName, random, link, maillink, "Generate");
                                
                                string queryString = "Update Link_Generation_Table set Url='" + updturl +"' where Candidate_Id = '" + NewCandidateId + "'";
                                using (SqlConnection upcon = new SqlConnection(constring))
                                {
                                    var id = HttpContext.Session.GetString("CUser");

                                    SqlCommand upcheck = new SqlCommand(queryString, upcon);
                                    upcon.Open();
                                    upcheck.ExecuteNonQuery();
                                    upcon.Close();
                                }
                                
                                ViewData["Message"] = "Link generated and an email is sent to the candidate";
                            }
                            else { ViewData["Message"] = "Unable to generate the link, Contact HR Team for resolution"; }
                        }
                    }

                }

            }
            return View();
        }


        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static List<char> characters = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                                                        'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                                                        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                        'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'
        };


        public static string GetURL()
        {
            string URL = "";
            Random rand = new Random();
            // run the loop till I get a string of 10 characters
            for (int i = 0; i < 11; i++)
            {
                // Get random numbers, to get either a character or a number...
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number
                    random = rand.Next(0, numbers.Count);
                    URL += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    URL += characters[random].ToString();
                }
            }

            return URL;
        }

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


        public string sendmail(string CEmail, string CName, string Ran, String Link, String MailLink, String Callingfrom)

        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            string encryptit = Library.EncryptDecrypt.encrypt(MailLink);
            string body = "";
            string br = "";
            string bold = "";
            string url = "";
            //Calling Generate
            if (Callingfrom == "Generate")
            {
                encryptit = Library.EncryptDecrypt.encrypt(MailLink);
                
                br = "";
                br += "<br>";
                bold = "<b style='font-size:16px;'>" + Ran + "</b>";

                //            string url = "<a href='http//localhost:7292/GenerateLink/CheckToken' class='a'> " + Link + " </a>";
                url = "<a href='https://localhost:7292/GenerateLink/CheckToken?" + encryptit + "'" + " class='a'> " + Link + " </a>";



                body += "<p>" + "Hello" + " " + CName + ", <br><br>  " + "Greetings from AWC Software! " + br + br +
                     "This is auto generated email. Please Click on the Link " + url + " to begin your on-boarding process. " + br + br + " Your Verification Code is : " + bold + " " + br + br + ""
                  + "</p><p>Note: Your password is the above mentioned Verification Code followed by the first four letters of your email address and the last four digits of the mobile number. The password is case sensitive (lowercase). Please do not include any special characters, spaces or salutations </ p>"
                  + "</p><p>Illustration:" + br + br + " Name: Sujit Sharma " + br + "Mail id: sujit.sharma96 @xyz.com " + br + "Phone #: 9876543210 " + br + "Verification code: 7654 " + br + "Then password will be: 7654SUJI3210" + br + " </p><p>Thank you and wishing you a great career with AWC Software!</p>";
            }



            //else Reinitiate
            else
            {
                encryptit = Library.EncryptDecrypt.encrypt(MailLink);
                
                br = "";
                br += "<br>";
                bold = "<b style='font-size:16px;'>" + Ran + "</b>";

                //            string url = "<a href='http//localhost:7292/GenerateLink/CheckToken' class='a'> " + Link + " </a>";
                url = "<a href='https://localhost:7292/GenerateLink/CheckToken?" + MailLink + "'" + " class='a'> " + Link + " </a>";



                body += "<p>" + "Hello" + " " + CName + ", <br><br>  " + "Greetings from AWC Software! " + br + br +
                     "This is auto generated email. Please Click on the Link " + url + " to begin your on-boarding process. " + br + br + " Use your old Verification Code for re-entry. " + br + br + "";

            }
                
//            string br = "";
//            br += "<br>";
//            string bold = "<b style='font-size:16px;'>" + Ran + "</b>";
//            string body = "";
////            string url = "<a href='http//localhost:7292/GenerateLink/CheckToken' class='a'> " + Link + " </a>";
//            string url = "<a href='https//localhost:7292/GenerateLink/CheckToken?" + encryptit + "'" + " class='a'> " + Link + " </a>";

//            body += "<p>" + "Hello" + " " + CName + ", <br><br>  " + "Greetings from AWC Software! " + br + br + "This is auto generated email. Please Click on the Link " + url + " to begin your on-boarding process. " + br + br + " Your Verification Code is : " + bold + " " + br + br + ""
//                 + "</p><p>Note: Your password is the above mentioned Verification Code followed by the first four letters of your email address and the last four digits of the mobile number. The password is case sensitive (uppercase). Please do not include any special characters, spaces or salutations </ p>"
//                 + "</p><p>Illustration:" + br + br + " Name: Sujit Sharma " + br + "Mail id: sujit.sharma96@xyz.com " + br + "Phone #: 9876543210 " + br + "Verification code: 7654 "  + br + "Then password will be: 7654SUJI3210"  + br + " </p><p>Thank you and wishing you a great career with AWC Software!</p>";



            MailAddress from = new MailAddress("akashchoudhary199404@gmail.com");
            MailAddress to = new MailAddress(CEmail);
            MailMessage message = new MailMessage(from, to);
            if (Callingfrom == "Generate") { 
                message.Subject = "On Boarding Form Link";
            }
            else {
                message.Subject = "On Boarding Form Link Reinitiated";
            }
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

            return encryptit;
        }

        [HttpGet]
        public IActionResult CheckToken()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckToken(LinkGenerationTable linkgeneration)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            int i;
            int candid = 0;
            string URL = Request.GetDisplayUrl().Split('?')[1];
            string decrypturl = Library.EncryptDecrypt.Decrypt(URL);

            var arrParamInfo = decrypturl.Split('&');
            for (i = 0; i < arrParamInfo.Length; i++)
            {
                var StrVal = arrParamInfo[i].Split('=');

                if (StrVal[0] == "" || StrVal[1] == "")
                {
                    ViewData["Error"] = "Invalid URL Used";

                }
                else
                {
                    if (candid == 0)
                    {   candid = Int32.Parse(StrVal[1]);
                        ViewBag.NewCId = StrVal[1];
                    }
                }
            }


            if (linkgeneration == null)
            {
                return NotFound();
            }
            else if (linkgeneration.VerificationCode == null)
            {
                ViewData["Error"] = "No Token Supplied.";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    var id = HttpContext.Session.GetString("CUser");
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_Verify_Token", con);
                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@Tokenid", linkgeneration.VerificationCode);

                    using (var reader = check.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i = Convert.ToInt32(reader["Result"]);
                            if (i == 0)
                            {
                                ViewData["Error"] = "Invalid Verification Code Supplied.";
                            }
                            else if (i == 40)
                            {
                                ViewData["Error"] = "Verification Code has been Expired, Contact HR Team for resolution";
                            }
                            else
                            {

                                reader.NextResult(); // Loop for the current status

                                if (reader.Read())
                                {
                                    i = Convert.ToInt32(reader["status"]);
                                    TempData["LinkStatus"] = i;

                                }

                                TempData["NewData"] = decrypturl;

                                return RedirectToAction("CandidateRegisterationDetail", "CandidateRegistration", new { id = ViewBag.NewCId });
                            }
                        }

                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ReinitiateCandidateLink(int id)
        {
            var retval3 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            using (SqlConnection con = new SqlConnection(constring))
            {
                int a = id;
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchGenerationDetails", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateId", a);

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        //      select Candidate_Name0, Candidate_Email1, Verification_Code2, Candidate_Contact_Number3, Link4, Date_Of_Joining5, Exp_Days6, CreatedBy7, Link_Expiration8,Offer_Date9,Url10 from Link_Generation_Table

                        while (reader.Read())
                            if (reader.GetString(1) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval3 = reader.GetString(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" +
                                    reader.GetString(3) + "|" + reader.GetString(4) + "|" + reader.GetDateTime(5) + "|" +
                                    reader.GetInt32(6) + "|" + reader.GetString(7) + "|" + reader.GetDateTime(8) + "|" + reader.GetDateTime(9) + "|" + reader.GetString(10) + "|" + reader.GetInt32(11) + "|" + reader.GetString(12);

                            }

                        reader.NextResult();


                        reader.Close();
                        con.Close();

                    }

                }
            }

            @ViewBag.Vals2 = retval3;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReinitiateCandidateLink(LinkGenerationTable linkgeneration)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            Random rand = new Random();
            string random = rand.Next(0, 10000).ToString("D4");

            string randomdb = random + linkgeneration.CandidateEmail.Substring(0, 4).ToUpper() + linkgeneration.CandidateContactNumber.Substring(linkgeneration.CandidateContactNumber.Length - 4);

            int i;

            if (linkgeneration == null)
            {
                return NotFound();
            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    //var id = HttpContext.Session.GetString("CUser");
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_ReinitiateCandidateLink", con);
                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@Candidate_ID", linkgeneration.CandidateId);
                    //check.Parameters.AddWithValue("@Url", linkgeneration.Url);
                    check.Parameters.AddWithValue("@ExpDate", linkgeneration.LinkExpiration);

                    check.Parameters.AddWithValue("@CreatedBy", "Admin");
                    check.Parameters.AddWithValue("exdays", linkgeneration.Exp_Days);

                    i = check.ExecuteNonQuery();
                    //Using 2 for the case, we are executing two queries in this Sp
                    if (i == 2)
                    {
                        sendmail(linkgeneration.CandidateEmail, linkgeneration.CandidateName, "", linkgeneration.Link, linkgeneration.Url, "Reinitiate");
                        ViewData["Message"] = "Link reinitiated and an email is sent to the candidate";
                    }//link generation controller

                }

            }
            return View();
        }
    }
}


