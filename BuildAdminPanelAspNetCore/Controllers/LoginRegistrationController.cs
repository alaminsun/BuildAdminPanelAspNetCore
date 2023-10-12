
using BuildAdminPanelAspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Systems.Universal;

namespace BuildAdminPanelAspNetCore.Controllers
{
    [Route("LoginRegistration")]
   
    public class LoginRegistrationController : Controller
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginRegistrationController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        ////SaPermission saPermission = new SaPermission();
        //SaOwnerData saOwnerData = new SaOwnerData();
        [Route("Login")]
        [Route("~/")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("CheckUser")]
        [HttpPost]
        public ActionResult CheckUser(LoginRegistrationModel master)
        {
            try
            {
                if ((master.UserID != null) && (master.Password != null))
                {
                    // var verifiedUserCredential = loginRegistrationDAO.CheckUserCredential().Where(m => m.UserID.Equals(master.UserID) && m.Password.Equals(master.Password)).FirstOrDefault();
                    var verifiedUserCredential = CheckUserCredential().Where(m => m.UserID.Equals(Encryption.Encrypt(master.UserID)) && m.Password.Equals(Encryption.Encrypt(master.Password))).FirstOrDefault();
                    if (verifiedUserCredential != null)
                    {

                        _httpContextAccessor.HttpContext.Session.SetString("UserID", verifiedUserCredential.UserID);
                        _httpContextAccessor.HttpContext.Session.SetString("enUserID", verifiedUserCredential.UserID);
                        _httpContextAccessor.HttpContext.Session.SetString("deUserID", Encryption.Decrypt(verifiedUserCredential.UserID));
                        _httpContextAccessor.HttpContext.Session.SetString("RoleID", verifiedUserCredential.RoleID);
                        _httpContextAccessor.HttpContext.Session.SetString("RoleName", verifiedUserCredential.RoleName);
                        _httpContextAccessor.HttpContext.Session.SetString("EmpID", verifiedUserCredential.EmpID.ToString());
                        _httpContextAccessor.HttpContext.Session.SetString("UserID", verifiedUserCredential.UserID);
                        if (!string.IsNullOrEmpty(verifiedUserCredential.EmpName))
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("EmpName", verifiedUserCredential.EmpName);
                        }
                        else
                        {
                            _httpContextAccessor.HttpContext.Session.SetString("EmpName", Encryption.Decrypt(verifiedUserCredential.UserID));
                        }

                        //session.SetString("COMPANY_NAME", "Square Pharmaceuticals Limited");
                        //session.SetString("ProjectName", "Square Regulatory Management System");
                        //session.SetString("DEV_BY", "Developed By : Square InformatiX Limited");

                        ////Session["EmpName"] = verifiedUserCredential.EmpName;
                        ////Session["SupervisorID"] = verifiedUserCredential.SupervisorID;
                        ////Session["SupervisorName"] = verifiedUserCredential.SupervisorName;
                        ////Session["Designation"] = verifiedUserCredential.Designation;
                        ////Session["EmploymentDate"] = verifiedUserCredential.EmploymentDate;
                        _httpContextAccessor.HttpContext.Session.SetString("IsLogged", MenuPopulate(verifiedUserCredential.UserID).ToString());

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.msg = "Incorrect User Credential!!!";
                        return View("Index");
                    }

                }

            }
            catch (Exception e)
            {
                ViewBag.msg = "Error Occurs!!!";//exceptionHandler.ErrorMsg(e);
            }

            return View("Index");
        }
        private List<LoginRegistrationModel> CheckUserCredential()
        {
            //string qry = "SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,e.EMPLOYEE_NAME," +
            //" u.NewPassword,u.OldPassword,ur.IsActive FROM Sa_UserInRole ur, Sa_UserCredential u,Sa_Role r,EMPLOYEE_INFO e Where ur.UserID=u.UserID and ur.RoleID=r.RoleID and UR.EMPID=E.ID and upper(ur.IsActive)=upper('true') ";
            string qry = "SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,e.EMPLOYEE_NAME, " +
                        " u.NewPassword,u.OldPassword,ur.IsActive FROM Sa_UserCredential u " +
                        " left join Sa_UserInRole ur on ur.USERID = u.USERID " +
                        " left join Sa_Role r on r.ROLEID = ur.ROLEID " +
                        " left join EMPLOYEE_INFO e on UR.EMPID = E.ID Where upper(ur.IsActive)= upper('true') ";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), qry);

            var item = (from DataRow row in dt.Rows
                        select new LoginRegistrationModel
                        {
                            UserID = row["UserID"].ToString(),
                            Password = row["NewPassword"].ToString(),
                            RoleID = row["RoleID"].ToString(),
                            RoleName = row["RoleName"].ToString(),
                            EmpID = Convert.ToInt32(row["EmpID"]),
                            EmpName = row["EMPLOYEE_NAME"].ToString(),
                            //SupervisorID = row["SupervisorID"].ToString(),
                            //SupervisorName = row["SupervisorName"].ToString(),
                            //Designation = row["Designation"].ToString(),
                            //EmploymentDate = row["EmploymentDate"].ToString(),
                            IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                        }).ToList();
            return item;
        }

        private bool MenuPopulate(string UserID)
        {
            if (saHelper.ProcedureExecuteFn1(dbConn.SAConnStrReader(), "", "Sa_Menu_SP", "pUserID", UserID))
            {
                return true;
            }
            return false;
        }
    }
}
