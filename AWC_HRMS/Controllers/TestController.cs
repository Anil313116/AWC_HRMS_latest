using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using AWC_HRMS.Models;

namespace AWC_HRMS_Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult TestUser()
        {

            return View();
        }

        public IActionResult Employee()
        {
            return View();
        }
        //EmployementList
        public JsonResult EmpList(int id)
        {
            return Json(ListAllEmployee(id));
        }
        //Add Employeement Detail
        public JsonResult EmpAdd(CandidateEmployement emp)
        {
            return Json(EmpAddInsert(emp));
        }
        //Edit Employeement Detail
        public JsonResult GetbyEmplID(int ID)
        {
            var Employee = ListAllEmployeeid(ID).Find(x => x.Id.Equals(ID));
            return Json(Employee);
        }

        //Update Emp
        public JsonResult EmpUpdate(CandidateEmployement emp)
        {
            return Json(EmpUpdateFirst(emp));
        }

        public JsonResult EmpUpdateChanges(CandidateEmployement emp)
        {
            return Json(EmpUpdateSecond(emp));
        }

        public JsonResult EmployeeDelete(int ID)
        {
            return Json(EmployeeDeleteDB(ID));
        }

        //public JsonResult List()
        //{
        //    return Json(empDB.ListAll());
        //}
        //public JsonResult Add(Employee emp)
        //{
        //    return Json(empDB.Add(emp));
        //}

        //public JsonResult GetbyID(int ID)
        //{
        //    var Employee = empDB.ListAll().Find(x => x.EmployeeID.Equals(ID));
        //    return Json(Employee);
        //}
        //public JsonResult Update(Employee emp)
        //{
        //    return Json(empDB.Update(emp));
        //}
        //public JsonResult Delete(int ID)
        //{
        //    return Json(empDB.Delete(ID));
        //}

        public List<CandidateEmployement> ListAllEmployee(int id)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            List<CandidateEmployement> lst = new List<CandidateEmployement>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee", con);
                com.Parameters.AddWithValue("@Id", id);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new CandidateEmployement
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        NameOfCompany = rdr["Company_Name"].ToString(),
                        DOJ = rdr["DOJ"].ToString(),
                        DOL = rdr["DOL"].ToString(),
                        Designation = rdr["Designation"].ToString(),
                        CTC = rdr["CTC"].ToString(),
                        RelatedDocument = rdr["Relieving_Letter"].ToString(),
                        Reason = rdr["Reason"].ToString(),
                        //Age = Convert.ToInt32(rdr["Age"]),
                        //State = rdr["State"].ToString(),
                        // Country = rdr["Country"].ToString(),
                    });
                }
                return lst;
            }
        }

        public List<CandidateEmployement> ListAllEmployeeid(int id)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            List<CandidateEmployement> lst = new List<CandidateEmployement>();
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployeeid", con);
                com.Parameters.AddWithValue("@Id", id);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new CandidateEmployement
                    {
                        Id = Convert.ToInt32(rdr["Id"]),
                        NameOfCompany = rdr["Company_Name"].ToString(),
                        DOJ = rdr["DOJ"].ToString(),
                        DOL = rdr["DOL"].ToString(),
                        Designation = rdr["Designation"].ToString(),
                        CTC = rdr["CTC"].ToString(),
                        RelatedDocument = rdr["Relieving_Letter"].ToString(),
                        Reason = rdr["Reason"].ToString(),
                        //Age = Convert.ToInt32(rdr["Age"]),
                        //State = rdr["State"].ToString(),
                        // Country = rdr["Country"].ToString(),
                    });
                }
                return lst;
            }
        }

        public int EmpAddInsert(CandidateEmployement emp)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            int i;
            //emp.CandidateId = 116;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@Action", "Insert");
                com.Parameters.AddWithValue("@CandidateId", emp.CandidateId);
                com.Parameters.AddWithValue("@Company_Name", emp.NameOfCompany);
                com.Parameters.AddWithValue("@DOJ", emp.DOJ);
                com.Parameters.AddWithValue("@DOL", emp.DOL);
                com.Parameters.AddWithValue("@Designation", emp.Designation);
                com.Parameters.AddWithValue("@CTC", emp.CTC);
                com.Parameters.AddWithValue("@RelatedDocument", emp.RelatedDocument);
                com.Parameters.AddWithValue("@Reason", emp.Reason);

                // com.Parameters.AddWithValue("@Country", emp.Country);

                i = com.ExecuteNonQuery();
            }
            return i;
        }


        public int EmpUpdateFirst(CandidateEmployement emp)
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            int i;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", emp.Id);
                com.Parameters.AddWithValue("@Company_Name", emp.NameOfCompany);
                com.Parameters.AddWithValue("@DOJ", emp.DOJ);
                com.Parameters.AddWithValue("@DOL", emp.DOL);
                com.Parameters.AddWithValue("@Designation", emp.Designation);
                com.Parameters.AddWithValue("@CTC", emp.CTC);
                com.Parameters.AddWithValue("@RelatedDocument", emp.RelatedDocument);
                com.Parameters.AddWithValue("@Reason", emp.Reason);


                i = com.ExecuteNonQuery();
            }
            return i;
        }


        public int EmpUpdateSecond(CandidateEmployement emp)
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            int i;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("UpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", emp.Id);
                com.Parameters.AddWithValue("@Company_Name", emp.NameOfCompany);
                com.Parameters.AddWithValue("@DOJ", emp.DOJ);
                com.Parameters.AddWithValue("@DOL", emp.DOL);
                com.Parameters.AddWithValue("@Designation", emp.Designation);
                com.Parameters.AddWithValue("@CTC", emp.CTC);
                com.Parameters.AddWithValue("@RelatedDocument", emp.RelatedDocument);
                com.Parameters.AddWithValue("@Reason", emp.Reason);


                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int EmployeeDeleteDB(int ID)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            int i;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }


    }
}
