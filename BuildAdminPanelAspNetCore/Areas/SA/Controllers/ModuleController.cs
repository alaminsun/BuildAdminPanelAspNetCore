using BuildAdminPanelAspNetCore.ActionFilter;
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    public class ModuleController : Controller
    {
       ModuleDAO primaryDAO = new ModuleDAO();
        // GET: /SA/Module/
        [ActionAuth]
        public ActionResult frmModule()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmHome"));
        }


        [HttpGet]
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
            ModelState.Remove("SoftwareID");
            if (ModelState.IsValid)
            {

                if (obj.ModuleID == "" || obj.ModuleID == null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    TempData["success"] = "Module created successfully";
                }
                else
                {
                    //_unitOfWork.Company.Update(obj);
                    primaryDAO.SaveUpdate(obj);
                    TempData["success"] = "Module updated successfully";
                }
                //_unitOfWork.Save();

                return RedirectToAction("frmSoftware");
            }
            return View(obj);

        }

     
    }
}