

using BuildAdminPanelAspNetCore.ActionFilter;
using BuildAdminPanelAspNetCore.Models.BEL;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using Systems.Universal;

namespace BuildAdminPanelAspNetCore.Areas.SA.Controllers
{
    [Area("SA")]
    [Route("SA/Form")]
    public class FormController : Controller
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        FormDAO primaryDAO = new FormDAO();
        // GET: /SA/Form/
        //[ActionAuth]
        //[Route("frmForm")]
        [Route("frmForm")]
        //[Route("~/")]
        public IActionResult frmForm()
        {
            if (HttpContext.Session.GetString("UserID") != null)
            {
                return View();
            }
            return Redirect(string.Format("~/Home/frmHome"));

            //return View();
        }

        //[HttpGet]
        //public ActionResult GetForm()
        //{
        //    var data = primaryDAO.GetFormList();
        //    return new JsonResult(data);
        //}



        [HttpPost]
        public ActionResult OperationsMode(FormBEL master)
        {
            try
            {
                if (primaryDAO.SaveUpdate(master))
                {
                    return Json(new { ID = primaryDAO.MaxID, Mode = primaryDAO.IUMode, Status = "Yes" });
                }
                else
                    return View("frmRole");
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