


using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Areas.SA.Models.DAL.DAO;

using Microsoft.AspNetCore.Mvc;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/Software")]
    public class SoftwareController : Controller
    {
        SoftwareDAO primaryDAO = new SoftwareDAO();
        //
        // GET: /SA/Software/
        //[ActionAuth]
        [Route("frmSoftware")]
        public ActionResult frmSoftware()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmHome"));
        }


        [HttpGet]
        [Route("GetSoftware")]
        public ActionResult GetSoftware()
        {
            var softwarelist = primaryDAO.GetSoftwareList();
            return Json(new { data = softwarelist });
        }



        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? id)
        {
            SoftwareBEL software = new();

            if (id == null || id == 0)
            {
                return View(software);
            }
            else
            {
                software = primaryDAO.GetSoftwareById(id);
                return View(software);
            }
        }

        [HttpPost]
        [Route("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(SoftwareBEL obj)
        {
            ModelState.Remove("SoftwareID");
            if (ModelState.IsValid)
            {

                if (obj.SoftwareID == "" || obj.SoftwareID == null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    TempData["success"] = "Menu created successfully";
                }
                else
                {
                    //_unitOfWork.Company.Update(obj);
                    primaryDAO.SaveUpdate(obj);
                    TempData["success"] = "Menu updated successfully";
                }
                //_unitOfWork.Save();

                return RedirectToAction("frmSoftware");
            }
            return View(obj);

        }

        [Route("delete")]
        public IActionResult Delete(int? id)
        {
            var obj = primaryDAO.GetSoftwareById(id);
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