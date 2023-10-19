using BuildAdminPanelAspNetCore.ActionFilter;
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/Module")]
    public class ModuleController : Controller
    {
       ModuleDAO primaryDAO = new ModuleDAO();
        // GET: /SA/Module/
        //[ActionAuth]
        [Route("frmModule")]
        public ActionResult frmModule()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmHome"));
        }


        [HttpGet]
        [Route("GetModule")]
        public ActionResult GetModule()
        {
            var moduleList = primaryDAO.GetModuleList();
            return Json(new { data = moduleList });
        }

        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? id)
        {
            ModuleBEL module = new();

            if (id == null || id == 0)
            {
                return View(module);
            }
            else
            {
                module = primaryDAO.GetModuleById(id);
                return View(module);
            }
        }

        [HttpPost]
        [Route("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ModuleBEL obj)
        {
            ModelState.Remove("ModuleID");
            if (ModelState.IsValid)
            {

                if (obj.ModuleID == "" || obj.ModuleID == null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    TempData["success"] = "submenu created successfully";
                }
                else
                {
                    //_unitOfWork.Company.Update(obj);
                    primaryDAO.SaveUpdate(obj);
                    TempData["success"] = "submenu updated successfully";
                }
                //_unitOfWork.Save();

                return RedirectToAction("frmModule");
            }
            return View(obj);

        }


        [Route("delete")]
        public IActionResult Delete(int? id)
        {
            var obj = primaryDAO.GetModuleById(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            primaryDAO.DeleteExecute(obj);
            return Json(new { success = true, message = "Delete Successful" });
            //TempData["success"] = "Menu deleted successfully";
            //return View(obj);
        }


    }
}