
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Systems.Universal;

namespace BuildAdminPanelAspNetCore.Controlles
{
    //[Route("home")]
    public class HomeController : Controller
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //[Route("index")]
        //[Route("~/")]
        public IActionResult Index()
        {
            return View();
        }
        //[Route("GetForm")]
        [HttpPost]
        public ActionResult GetForm(string FormName)
        {
            //Session["FormName"] = "";
            HttpContext.Session.SetString("FormName", "");
            FormName = (FormName == null || FormName == "") ? "Home" : FormName;
            var Data = FormName;
            //Session["FormName"] = FormName;
            HttpContext.Session.SetString("FormName", Data);
            string Qry = "Select RefID from Sa_Menu Where NodeName='" + FormName + "' and UserID='" + HttpContext.Session.GetString("UserID") + "'";
            //Session["RefID"] = saHelper.GetValueFn(dbConn.SAConnStrReader(), Qry);
            string refId = saHelper.GetValueFn(dbConn.SAConnStrReader(), Qry);
            HttpContext.Session.SetString("RefID", refId);
            return new JsonResult(Data);
        }
        //[Route("LoadMenu")]
        public ActionResult LoadMenu(int? id)
        {
            string userId = HttpContext.Session.GetString("UserID");
            var nodes = GetMenuItemDataTable(userId);
            //return Json(nodes, JsonRequestBehavior.AllowGet);
            return new JsonResult(nodes);
        }
        private List<MenuItemView> GetMenuItemDataTable(string UserID)
        {
            List<MenuItemView> listMenuItemViews = new List<MenuItemView>();

            //string Qry = " Select ID,NodeName,DECODE(subStr(FormURL,1,3),'frm',subStr(FormURL,4,length(FormURL)-3),FormURL) Controller,NodeLevel,FormURL,MENUIMAGE,RefID,UserID,SoftwareName,SoftwareShortName," +
            //            " TO_Number( CASE WHEN LENGTH(RefID)<LENGTH(UserID)+3  THEN SUBSTR(RefID,1,2) " +
            //            " WHEN LENGTH(RefID)<LENGTH(UserID)+5  THEN SUBSTR(RefID,1,4) " +
            //            " WHEN LENGTH(RefID)<LENGTH(UserID)+8 THEN SUBSTR(RefID,1,7) " +
            //            " END)  As SL," +
            //            " TO_Number(CASE   WHEN LENGTH(RefID)<LENGTH(UserID)+3  THEN '0' " +
            //            " WHEN LENGTH(RefID)<LENGTH(UserID)+5  THEN SUBSTR(RefID,1,2) " +
            //            " WHEN LENGTH(RefID)<LENGTH(UserID)+8  THEN SUBSTR(RefID,1,4) " +
            //            " END)  As Parent " +
            //            " FROM Sa_Menu m,Sa_Software d Where SUBSTR(m.RefID,0,2)=d.SoftwareID and UserID='" + UserID + "'  Order By NodeLevel,SL";
            string Qry = "SELECT ID, NodeName, "+
   " CASE "+
   "	WHEN SUBSTRING(FormURL, 1, 3) = 'frm' "+
   "   THEN SUBSTRING(FormURL, 4, LEN(FormURL) - 3) "+
   "   ELSE "+
   "     FormURL "+
   " END AS Controller, NodeLevel, FormURL, MENUIMAGE, RefID, UserID, SoftwareName, SoftwareShortName, "+
   " CAST( CASE "+
   "         WHEN LEN(RefID) < LEN(UserID) + 3 THEN SUBSTRING(RefID, 1, 2) "+
   "         WHEN LEN(RefID) < LEN(UserID) + 5 THEN SUBSTRING(RefID, 1, 4) "+
   "         WHEN LEN(RefID) < LEN(UserID) + 8 THEN SUBSTRING(RefID, 1, 7) "+
    "    END AS INT  ) AS SL, "+
   " CAST( "+
   "     CASE "+
   "         WHEN LEN(RefID) < LEN(UserID) + 3 THEN '0' "+
   "         WHEN LEN(RefID) < LEN(UserID) + 5 THEN SUBSTRING(RefID, 1, 2) "+
   "         WHEN LEN(RefID) < LEN(UserID) + 8 THEN SUBSTRING(RefID, 1, 4) "+
   "     END AS INT ) AS Parent "+
" FROM  Sa_Menu m, Sa_Software d "+
" WHERE  SUBSTRING(m.RefID, 1, 2) = d.SoftwareID AND UserID = '" + UserID + "'"+
" ORDER BY NodeLevel, SL";


            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MenuItemView menuItemView = new MenuItemView();
                menuItemView.MenuID = Convert.ToInt64(dt.Rows[i]["SL"].ToString());
                menuItemView.MenuName = dt.Rows[i]["NodeName"].ToString();
                menuItemView.ParentID = Convert.ToInt64(dt.Rows[i]["Parent"].ToString()) == 0 ? default(Int64?) : Convert.ToInt64(dt.Rows[i]["Parent"].ToString());
                //if (!dt.Rows[i]["FormURL"].ToString().Contains("/"))
                //{
                //    menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["Controller"].ToString().Replace("View", "") + "/" + dt.Rows[i]["FormURL"].ToString();
                //}
                //else if()
                //{
                //    menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["FormURL"].ToString();
                //}

                if (dt.Rows[i]["Controller"].ToString().Contains("View"))
                {
                    menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["Controller"].ToString().Replace("View", "") + "/" + dt.Rows[i]["FormURL"].ToString();
                }
                else if (dt.Rows[i]["Controller"].ToString().Contains("RptPdf"))
                {
                    menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["Controller"].ToString().Replace("Pdf", "") + "/" + dt.Rows[i]["FormURL"].ToString();
                }
                else if (dt.Rows[i]["Controller"].ToString().Contains("Rpt"))
                {
                    menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["Controller"].ToString().Replace("Rpt", "") + "/" + dt.Rows[i]["FormURL"].ToString();
                }
                else
                {
                    menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["Controller"].ToString() + "/" + dt.Rows[i]["FormURL"].ToString();
                }
                //menuItemView.FormURL = "" + "/" + dt.Rows[i]["SoftwareShortName"].ToString() + "/" + dt.Rows[i]["Controller"].ToString().Replace("View", "").Replace("Rpt","") + "/" + dt.Rows[i]["FormURL"].ToString();
                menuItemView.MenuImage = dt.Rows[i]["MENUIMAGE"].ToString();
                //menuItemView.HasChild = true;
                listMenuItemViews.Add(menuItemView);
            }
            List<MenuItemView> parentMenuTree = GetMenuTreeList(listMenuItemViews, null);
            return parentMenuTree.ToList();
        }
        private List<MenuItemView> GetMenuTreeList(List<MenuItemView> list, Int64? parentID)
        {
            return list.Where(x => x.ParentID == parentID).Select(x => new MenuItemView()
            {
                MenuID = x.MenuID,
                MenuName = x.MenuName,
                ParentID = x.ParentID,
                FormURL = x.FormURL,
                MenuImage = x.MenuImage,
                //HasChild = x.HasChild,
                MenuItemList = GetMenuTreeList(list, x.MenuID)
            }).ToList();
        }
    }
}
