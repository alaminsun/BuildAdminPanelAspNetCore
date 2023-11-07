using BuildAdminPanelAspNetCore.ActionFilter;
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using BuildAdminPanelAspNetCore.Universal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;


namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/Form")]
    
    public class FormController : Controller
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        FormDAO primaryDAO = new FormDAO();
        // GET: /SA/Form/

        //[Route("frmForm")]
        
        [Route("frmForm")]
        //[Route("~/")]
        [ActionAuth]
        public IActionResult frmForm()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmHome"));

            //return View();
        }

        [HttpGet]
        [Route("GetForm")]
        public ActionResult GetForm()
        {
            var formlist = primaryDAO.GetFormList();
            return Json(new { data = formlist });
            //return new JsonResult(data);
        }
        [HttpGet]
        [Route("Upsert")]
        public IActionResult Upsert(int? id)
        {
            FormBEL form = new();

            if (id == null || id == 0)
            {
                return View(form);
            }
            else
            {
                form = primaryDAO.GetForm(id);
                return View(form);
            }
        }

        [HttpPost]
        [Route("Upsert")]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(FormBEL obj)
        {
            ModelState.Remove("FormID");
            if (ModelState.IsValid)
            {

                if (obj.FormID == "" || obj.FormID == null)
                {
                    //_unitOfWork.Company.Add(obj);
                    primaryDAO.SaveUpdate(obj);

                    TempData["success"] = "Form created successfully";
                }
                else
                {
                    //_unitOfWork.Company.Update(obj);
                    primaryDAO.SaveUpdate(obj);
                    TempData["success"] = "Form updated successfully";
                }
                //_unitOfWork.Save();

                return RedirectToAction("frmForm");
            }
            return View(obj);

        }

        //[HttpPost]
        //public ActionResult OperationsMode(FormBEL master)
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
        
        //[HttpDelete]
        [Route("delete")]
        public IActionResult Delete(int? id)
        {
            var obj = primaryDAO.GetForm(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            primaryDAO.DeleteExecute(obj);
            return Json(new { success = true, message = "Delete Successful" });

        }
    }
}