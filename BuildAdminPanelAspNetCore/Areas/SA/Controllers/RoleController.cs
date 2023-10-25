

using BuildAdminPanelAspNetCore.ActionFilter;
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using Microsoft.AspNetCore.Mvc;
using RBuildAdminPanelAspNetCore.Models.DAL.DAO;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/Role")]
    public class RoleController : Controller
    {

        RoleDAO primaryDAO = new RoleDAO();
        //
        // GET: /SA/Role/

        //[ActionAuth]
        [Route("frmRole")]
        public ActionResult frmRole()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmRole"));
        }


        [HttpGet]
        [Route("GetRole")]
        public ActionResult GetRole()
        {
            var roleList = primaryDAO.GetRoleList();
            return Json(new { data = roleList });

        }

        [HttpGet]
        public ActionResult GetRoleInSoftwareModuleMapping()
        {
            var roleinSoft = primaryDAO.GetRoleInSoftwareModuleMappingList();
            return Json(new { data = roleinSoft });
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? id)
        {
            RoleBEL module = new();

            if (id == null || id == 0)
            {
                return View(module);
            }
            else
            {
                module = primaryDAO.GetRoleById(id);
                return View(module);
            }
        }

        [HttpPost]
        [Route("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RoleBEL obj)
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


        //[Route("delete")]
        //public IActionResult Delete(int? id)
        //{
        //    var obj = primaryDAO.GetModuleById(id);
        //    if (obj == null)
        //    {
        //        return Json(new { success = false, message = "Error while deleting" });
        //    }
        //    primaryDAO.DeleteExecute(obj);
        //    return Json(new { success = true, message = "Delete Successful" });
        //    //TempData["success"] = "Menu deleted successfully";
        //    //return View(obj);
        //}
    }
}