using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/UserInRole")]
    public class UserInRoleController : Controller
    {
        UserInRoleDAO primaryDAO = new UserInRoleDAO();
        //[ActionAuth]
        [Route("frmUserInRole")]
        public ActionResult frmUserInRole()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmUserInRole"));
        }

        [HttpGet]
        [Route("GetEmployee")]
        public ActionResult GetEmployee()
        {
            var employeeList = primaryDAO.GetEmployeeList();
            return Json(new { data = employeeList });
        }

        [HttpGet]
        [Route("GetEmployeeNotYetAssigned")]
        public ActionResult GetEmployeeNotYetAssigned()
        {
            var employeesNotYetAssigned = primaryDAO.GetEmployeeNotYetAssignedList();
            return Json(new { data = employeesNotYetAssigned});
        }

        [HttpPost]
        [Route("GetBuyerYetAssigned")]
        public ActionResult GetBuyerYetAssigned(string EmpID)
        {
            var buyerYetAssignedList = primaryDAO.GetBuyerYetAssignedList(EmpID);
            return Json(new { data = buyerYetAssignedList });
        }
        [HttpGet]
        [Route("GetBuyer")]
        public ActionResult GetBuyer()
        {
            var buyerList = primaryDAO.GetBuyerList();
            return Json(new { data = buyerList });
        }

        [HttpGet]
        [Route("GetUser")]
        public ActionResult GetUser()
        {
            var userList = primaryDAO.GetUserList();
            return Json(new { data = userList });
        }
        [HttpGet]
        [Route("GetUserInRole")]
        public ActionResult GetUserInRole()
        {
            var userInRoleList = primaryDAO.GetUserInRoleList();
            return Json(new { data = userInRoleList });
        }
        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? id)
        {
            UserInRoleBEL userInRole = new();

            if (id == null || id == 0)
            {
                return View(userInRole);
            }
            else
            {
                //form = primaryDAO.GetForm(id);
                //return View(form);
                return View();
            }
        }
        [HttpPost]
        [Route("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UserInRoleBEL obj)
        {
            ModelState.Remove("RoleID");
            if (ModelState.IsValid)
            {

                if (obj.RoleID == "" || obj.RoleID == null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    TempData["success"] = "role created successfully";
                }
                else
                {
                    //_unitOfWork.Company.Update(obj);
                    primaryDAO.SaveUpdate(obj);
                    TempData["success"] = "role updated successfully";
                }
                //_unitOfWork.Save();

                return RedirectToAction("frmRole");
            }
            return View(obj);

        } 
        //[HttpPost]
        //public ActionResult OperationsMode(UserInRoleBEL master)
        //{
        //    try
        //    {
        //       // var eu = Systems.Universal.Encryption.Encrypt(master.UserID);
        //        if (primaryDAO.SaveUpdate(master))//primaryDAO.SaveUpdate(master)
        //        {
        //            return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
        //        }
        //        else
        //            return View();
        //    }
        //    catch (Exception e)
        //    {
        //        if (e.Message.Substring(0, 9) == "ORA-00001")
        //            return Json(new { Status = "Error:ORA-00001,Data already exists!" });//Unique Identifier.
        //        else if (e.Message.Substring(0, 9) == "ORA-02292")
        //            return Json(new { Status = "Error:ORA-02292,Data already exists!" });//Child Record Found.
        //        else if (e.Message.Substring(0, 9) == "ORA-12899")
        //            return Json(new { Status = "Error:ORA-12899,Data Value Too Large!" });//Value Too Large.
        //        else
        //            return Json(new { Status = "! Error : Error Code:" + e.Message.Substring(0, 9) });//Other Wise Error Found

        //    }
        //}

    }
}