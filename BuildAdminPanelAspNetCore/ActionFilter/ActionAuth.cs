using BuildAdminPanelAspNetCore.Universal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace BuildAdminPanelAspNetCore.ActionFilter
{
    public class ActionAuth : ActionFilterAttribute
    {
        private IHttpContextAccessor _httpContextAccessor;
        public ActionAuth(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routingValues = filterContext.RouteData.Values;
            var currentArea = filterContext.RouteData.DataTokens["area"] ?? string.Empty;
            var currentController = (string)routingValues["controller"] ?? string.Empty;
            var currentAction = (string)routingValues["action"] ?? string.Empty;

            //HttpContext ctx = HttpContext.Current;
            //_httpContextAccessor.HttpContext.Session.GetString("UserID");
            ;
            if (!Convert.ToBoolean(_httpContextAccessor.HttpContext.Session.GetString("IsLogged")))
            {
                String redirectTo = string.Format("~/LoginRegistration/Index"); //string.Format("~/Home/SessionOutMsg");
                filterContext.Result = new RedirectResult(redirectTo);
            }
            else
            {
                var isExists = MenuExist(_httpContextAccessor.HttpContext.Session.GetString("IsLogged") as string, currentAction);
                if (isExists) return;
                String redirectTo = string.Format("~/LoginRegistration/Index");
                filterContext.Result = new RedirectResult(redirectTo);
            }
            base.OnActionExecuting(filterContext);
        }
        private bool MenuExist(string userId, string url)
        {
            string qry = string.Format("SELECT NODENAME FROM SA_MENU WHERE UserID='{0}' and FORMURL='{1}'", userId, url);
            DataTable dt = new SaHelper().DataTableFn(new DataBaseConnection().SAConnStrReader(), qry);
            //HttpContext.Current.Session["FormNameTitle"] = "";
            _httpContextAccessor.HttpContext.Session.SetString("FormNameTitle", "");
            if (dt.Rows.Count > 0)
            {
                //HttpContext.Current.Session["FormNameTitle"] = dt.Rows[0]["NODENAME"];
                _httpContextAccessor.HttpContext.Session.SetString("FormNameTitle", dt.Rows[0]["NODENAME"].ToString());
                return true;
            }
            else
            {
               
               return  false;
            }
           
        }
    }
}