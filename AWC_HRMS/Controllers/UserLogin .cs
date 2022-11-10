using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AWC_HRMS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using System;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using ForgotPasswordModel = AWC_HRMS.Models.ForgotPasswordModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AWC_HRMS_Web.Controllers
{
    public class UserLogin : Controller
    {
        

        [HttpGet]
        public IActionResult CheckLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckLogin(UserMasterVM userMastervm)
        {
            
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            int i;


            //if (userMastervm == null)
            //{
            //    return NotFound();

            //}
            //else
            //{
                try
                {
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();

                        SqlCommand check = new SqlCommand("Usp_Check_User_Login", con);
                        check.CommandType = CommandType.StoredProcedure;
                        check.Parameters.AddWithValue("@Empid", userMastervm.UserMaster.UserName);
                        check.Parameters.AddWithValue("@Password", userMastervm.UserMaster.Password);
                        using (var reader = check.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                i = Convert.ToInt32(reader["Result"]);
                                if (i == 0)
                                {
                                    ViewData["Error"] = "Invalid Username or Password";
                                }
                                else
                                {
                                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userMastervm.UserMaster.UserName) }, CookieAuthenticationDefaults.AuthenticationScheme);
                                    var Principal = new ClaimsPrincipal(identity);
                                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal);

                                    HttpContext.Session.SetString("CUser", Convert.ToString(userMastervm.UserMaster.UserName));

                                    return RedirectToAction("Index", "Dashboard");
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return View("DatabaseError");
                }

            //}

            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("CheckLogin", "UserLogin");
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                int i;
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Usp_UserEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Employee_Name", model.UserName);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i = Convert.ToInt32(reader["Result"]);
                            if (i == 1)
                            {

                                sendmail(model.UserName, model.Email);
                                return Json(new { data = "EXIST" });

                            }
                            else
                            {
                                return Json(new { data = "NOTEXIST" });
                            }
                        }
                    }
                    con.Close();
                }

            }
            return View();
        }

        public void sendmail(String name, string Email)
        {

            string em = Library.EncryptDecrypt.encrypt(Email);
            string br = "";
            br += "<br>";
            string body = "";
            string Gen_Link = "<a href = 'https://localhost:7292/UserLogin/ResetPassword?id="+em+"'> Forgot Password </a>";

            body += "<p>" + "Hello" + " " + name + " " + "Greetings of the Day, " + br + "This is auto generated email, Please Click on the Link to regenerate your password " + br + Gen_Link + " " + br + "" + br + "Thankyou."
+ "</p>";

            MailAddress from = new MailAddress("gaurav.dwivedi167@gmail.com");
            MailAddress to = new MailAddress(Email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Reset Password Link";
            message.IsBodyHtml = true;
            message.Body = body;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("gaurav.dwivedi167@gmail.com", "jgmrvwfknrihexyz"),
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


        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            string b = Library.EncryptDecrypt.Decrypt(id);
            TempData["id"] = b;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(AWC_HRMS.Models.ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                int i;
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_EmployeeResetNewPassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@NewPassword", model.Password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", model.ConfirmPassword);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction("CheckLogin");
                }

            }
            return View();
        }

    }
}

