using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Areas.SA.Models.ViewModels;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/UserInRole")]
    public class UserInRoleController : Controller
    {
        UserInRoleDAO primaryDAO = new UserInRoleDAO();
        RoleDAO roleDAO = new RoleDAO();
        UserInRoleViewModel userInRoleVM = new UserInRoleViewModel();
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
        public ActionResult GetEmployeeNotYetAssigned(string roleId)
        {
            //var employeesNotYetAssigned = primaryDAO.GetEmployeeNotYetAssignedList(roleId);
            //return Json(new { data = employeesNotYetAssigned});
            var roletoassign = primaryDAO.GetEmployeeNotYetAssignedList(roleId).Select(c => new SelectListItem
            {
                Value = c.EmpID.ToString(),
                Text = c.EmpName
            }).ToList();

            //return Json(roleList);
            return Json(new { data = roletoassign });
        }

        //[HttpPost]
        //[Route("GetBuyerYetAssigned")]
        //public ActionResult GetBuyerYetAssigned(string EmpID)
        //{
        //    var buyerYetAssignedList = primaryDAO.GetBuyerYetAssignedList(EmpID);
        //    return Json(new { data = buyerYetAssignedList });
        //}
        //[HttpGet]
        //[Route("GetBuyer")]
        //public ActionResult GetBuyer()
        //{
        //    var buyerList = primaryDAO.GetBuyerList();
        //    return Json(new { data = buyerList });
        //}

        [HttpGet]
        [Route("GetUser")]
        public ActionResult GetUser()
        {
            var userList = primaryDAO.GetUserList();
            return Json(new { data = userList });
        }
        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            //var countries = // Retrieve countries from your data source
            var roleList = roleDAO.GetRoleList().Select(c => new SelectListItem
            {
                Value = c.RoleID.ToString(),
                Text = c.RoleName
            }).ToList();

            //return Json(roleList);
            return Json(new { data = roleList });
        }


        [HttpGet]
        [Route("GetUserInRole")]
        public ActionResult GetUserInRole()
        {
            var userInRoleList = primaryDAO.GetUserInRoleList();
            return Json(new { data = userInRoleList });
            //return new JsonResult(userInRoleList);
        }
        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(string id)
        {
            //UserInRoleBEL userInRole = new();
            //var roleId = userInRole.userInRoleBEL.RoleID
            UserInRoleBEL userInRole = new();
            {
                //userInRole = new(),
                userInRole.RoleList = roleDAO.GetRoleList().Select(c => new SelectListItem
                {
                    Value = c.RoleID.ToString(),
                    Text = c.RoleName
                }).ToList();
                userInRole.EmpList = primaryDAO.GetEmployeeNotYetAssignedList(userInRole.RoleID).Select(c => new SelectListItem
                {
                    Value = c.EmpID.ToString(),
                    Text = c.EmpName
                }).ToList();
    };

            if (id == "" || id == null)
            {
                return View(userInRole);
            }
            else
            {
                userInRole = primaryDAO.GetUserInRoleById(id);
                //return View(form);
                return View(userInRole);
            }
        }
        [HttpPost]
        [Route("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(UserInRoleBEL obj)
        {
            ModelState.Remove("UserID");
            if (ModelState.IsValid)
            {

                if (obj.UserID != "" || obj.UserID != null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    TempData["success"] = "user is assigned to role successfully";
                }
                //else
                //{
                //    //_unitOfWork.Company.Update(obj);
                //    primaryDAO.SaveUpdate(obj);
                //    TempData["success"] = "role updated successfully";
                //}
                //_unitOfWork.Save();

                return RedirectToAction("frmUserInRole");
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