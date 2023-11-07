using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Areas.SA.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/RoleInFormPermission")]
    public class RoleInFormPermissionController : Controller
    {
        RoleInFormDAO primaryDAO = new RoleInFormDAO();

        // GET: /SA/RoleInSoftwareModuleFormMapping/
        //[ActionAuth]
        [Route("frmRoleInFormPermission")]
        public ActionResult frmRoleInFormPermission()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmRole"));
        }
        

        [HttpPost]
        [Route("GetRoleInFormPermissionList")]
        public ActionResult GetRoleInFormPermissionList(string RoleID)
        {
            var roleInFormPermissionList = primaryDAO.GetRoleInFormPermissionList(RoleID);
            //return Json(model, JsonRequestBehavior.AllowGet);
            return Json(data: roleInFormPermissionList);
        }
        [HttpPost]
        [Route("OperationsMode")]
        public ActionResult OperationsMode(RoleInFormMaster master)
        {
            try
            {

                if (primaryDAO.SaveUpdate(master))
                {
                    return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmRoleInFormPermission");
                //return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
            }
            catch (Exception e)
            {
                if (e.Message.Substring(0, 9) == "ORA-00001")
                    return Json(new { Status = "Error:ORA-00001,Data already exists!" });//Unique Identifier.
                else if (e.Message.Substring(0, 9) == "ORA-02292")
                    return Json(new { Status = "Error:ORA-02292,Data already exists!" });//Child Record Found.
                else if (e.Message.Substring(0, 9) == "ORA-12899")
                    return Json(new { Status = "Error:ORA-12899,Data Value Too Large!" });//Value Too Large.
                else
                    return Json(new { Status = "! Error : Error Code:" + e.Message.Substring(0, 9) });//Other Wise Error Found

            }

        }
	}
}