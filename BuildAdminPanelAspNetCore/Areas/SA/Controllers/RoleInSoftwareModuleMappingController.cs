using BuildAdminPanelAspNetCore.ActionFilter;
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Areas.SA.Models.DAL.DAO;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/RoleInSoftwareModuleMapping")]
    public class RoleInSoftwareModuleMappingController : Controller
    {
        RoleInSoftwareModuleDAO primaryDAO = new RoleInSoftwareModuleDAO();
        //
        // GET: /SA/RoleInSoftwareModuleMapping/
        //[ActionAuth]
        [Route("frmRoleInSoftwareModuleMapping")]
        public ActionResult frmRoleInSoftwareModuleMapping()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmRole"));
        }
        [HttpGet]
        [Route("GetRoleInSoftwareModuleMappingList")]
        public ActionResult GetRoleInSoftwareModuleMappingList(string RoleID)
        {
            var roleInSoftwareModuleMappingList = primaryDAO.GetRoleInSoftwareModuleMapping(RoleID);
            return Json(new { data = roleInSoftwareModuleMappingList });
        }

        //[HttpGet]
        //[Route("Upsert")]
        //public IActionResult Upsert(string id)
        //{
        //    //UserInRoleBEL userInRole = new();
        //    //var roleId = userInRole.userInRoleBEL.RoleID
        //    UserInRoleBEL userInRole = new();
        //    {
        //        //userInRole = new(),
        //        userInRole.RoleList = roleDAO.GetRoleList().Select(c => new SelectListItem
        //        {
        //            Value = c.RoleID.ToString(),
        //            Text = c.RoleName
        //        }).ToList();
        //        userInRole.EmpList = primaryDAO.GetEmployeeNotYetAssignedList(userInRole.RoleID).Select(c => new SelectListItem
        //        {
        //            Value = c.EmpID.ToString(),
        //            Text = c.EmpName
        //        }).ToList();
        //    };

        //    if (id == "" || id == null)
        //    {
        //        return View(userInRole);
        //    }
        //    else
        //    {
        //        userInRole = primaryDAO.GetUserInRoleById(id);
        //        //return View(form);
        //        return View(userInRole);
        //    }
        //}
        //[HttpPost]
        //[Route("Upsert")]
        //[ValidateAntiForgeryToken]
        //public IActionResult Upsert(UserInRoleBEL obj)
        //{
        //    ModelState.Remove("UserID");
        //    if (ModelState.IsValid)
        //    {

        //        if (obj.UserID != "" || obj.UserID != null)
        //        {
        //            //_unitOfWork.Company.Add(obj);
        //            primaryDAO.SaveUpdate(obj);

        //            TempData["success"] = "user is assigned to role successfully";
        //        }
        //        //else
        //        //{
        //        //    //_unitOfWork.Company.Update(obj);
        //        //    primaryDAO.SaveUpdate(obj);
        //        //    TempData["success"] = "role updated successfully";
        //        //}
        //        //_unitOfWork.Save();

        //        return RedirectToAction("frmUserInRole");
        //    }
        //    return View(obj);

        //}

        //public ActionResult OperationsMode(RoleInSoftwareModuleBEL master)
        //{
        //    try
        //    {
        //        if (primaryDAO.SaveUpdate(master))
        //        {
        //            return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
        //        }
        //        else
        //            return View("frmRole");
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