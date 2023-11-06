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
            return Json(data:roleInSoftwareModuleMappingList);
        }
        [HttpPost]
        [Route("Upsert")]
        //[ValidateAntiForgeryToken]
        public ActionResult Upsert(RoleInSoftwareMasterBEL obj)
        {

            if (ModelState.IsValid)
            {

                if (obj.RoleID != "" || obj.RoleID != null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    //TempData["success"] = "saved successfully";
                    return new JsonResult(new { Status = "Yes" });
                }
                //else
                //{
                //    //_unitOfWork.Company.Update(obj);
                //    primaryDAO.SaveUpdate(obj);
                //    TempData["success"] = "role updated successfully";
                //}
                //_unitOfWork.Save();

                return RedirectToAction("frmRoleInSoftwareModuleMapping");
                //return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
               
            }
            return View(obj);

        }

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