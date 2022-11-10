using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AWC_HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWC_HRMS_Web.Controllers
{
    public class UserRegistration : Controller
    {
        // GET: UserRegistration
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRegistration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegistration/Create
        public IActionResult CreateUpdateUser(int Id)
        {
            UserMasterVM vm = new UserMasterVM();
            if(Id == 0)
            {
                return View(vm);
            }
            else
            {
                using(var _context=new AWC_HRMSContext())
                {
                    vm.UserMaster = (from users in _context.UserMasters
                                     where users.UserId == Id
                                     select users).FirstOrDefault();
                }
                return View(vm);
            }


        }

        // POST: UserRegistration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdateUser(UserMasterVM userMastervm, string Admin,string SuperAdmin, String HR)
        {
            try
            {
                int AdminVal;
                if (Admin == "true")
                {
                    AdminVal = 1;
                    
                }
                else
                {
                    AdminVal = 0;

                }

                int AdminVal1;
                if (SuperAdmin == "true")
                {
                    AdminVal1 = 1;

                }
                else
                {
                    AdminVal1 = 0;

                }

                int AdminVal2;
                if (HR == "true")
                {
                    AdminVal2 = 1;

                }
                else
                {
                    AdminVal2 = 0;

                }

                int i;
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                //if(ModelState.IsValid)
                //{
                    if (userMastervm.UserMaster.UserId == 0)
                    {
                        using (SqlConnection con = new SqlConnection(constring))
                        {
                            con.Open();
                            SqlCommand add = new SqlCommand("Usp_CreateUpdateUser", con);
                            add.CommandType = CommandType.StoredProcedure;

                            add.Parameters.AddWithValue("@userid", '0');
                            add.Parameters.AddWithValue("@RoleId", '1');
                            add.Parameters.AddWithValue("@userName", userMastervm.UserMaster.UserName);
                            add.Parameters.AddWithValue("@Password", userMastervm.UserMaster.Password);
                            add.Parameters.AddWithValue("@ConfirmPassword", userMastervm.UserMaster.ConfirmPassword);
                            add.Parameters.AddWithValue("@Email", userMastervm.UserMaster.Email);
                            add.Parameters.AddWithValue("@ContactNo", userMastervm.UserMaster.ContactNo);
                            add.Parameters.AddWithValue("@IsInterestedInAdmin", AdminVal);
                            add.Parameters.AddWithValue("@IsInterestedInSuperAdmin", AdminVal1);
                            add.Parameters.AddWithValue("@IsInterestedInHR", AdminVal2);

                            //add.Parameters.AddWithValue("@RoleId", userMastervm.UserMaster.RoleId);

                            i = add.ExecuteNonQuery();
                            if(i== -1)
                            {
                                TempData["Success"] = "User Created Successfully";
                                return RedirectToAction("CheckLogin", "UserLogin");
                            }

                        }

                    //return RedirectToAction("Index", "UserLogin");
                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection(constring))
                        {
                            con.Open();
                            SqlCommand add = new SqlCommand("Usp_CreateUpdateUser", con);
                            add.CommandType = CommandType.StoredProcedure;

                            add.Parameters.AddWithValue("@userid", '0');
                            add.Parameters.AddWithValue("@RoleId", '1');
                            add.Parameters.AddWithValue("@userName", userMastervm.UserMaster.UserName);
                            //add.Parameters.AddWithValue("@Password", userMastervm.UserMaster.Password);
                            add.Parameters.AddWithValue("@IsInterestedInAdmin", AdminVal);
                            add.Parameters.AddWithValue("@IsInterestedInSuperAdmin", AdminVal1);
                            add.Parameters.AddWithValue("@IsInterestedInHR", AdminVal2);

                            //add.Parameters.AddWithValue("@RoleId", userMastervm.UserMaster.RoleId);

                            i = add.ExecuteNonQuery();
                          
                            if (i == 2)
                            {
                                TempData["Success"] = "User Updated Successfully";
                            }


                        }
                    

                    }
                //return RedirectToAction("Index", "UserLogin");





                //else
                //{
                //    return View(userMastervm);
                //}

                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: UserRegistration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegistration/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id)
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

        // GET: UserRegistration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegistration/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
