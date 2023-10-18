using BuildAdminPanelAspNetCore.Universal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace BuildAdminPanelAspNetCore.ActionFilter
{

    public class ActionAuthAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContextAccessor = (IHttpContextAccessor)context.HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor));
            var routingValues = context.RouteData.Values;
            var currentArea = context.RouteData.DataTokens["area"] as string ?? string.Empty;
            var currentController = routingValues["controller"] as string ?? string.Empty;
            var currentAction = routingValues["action"] as string ?? string.Empty;

            var session = context.HttpContext.Session;
            if (!Convert.ToBoolean(session.GetString("IsLogged")))
            {
                string redirectTo = $"~/LoginRegistration/Index";
                context.Result = new RedirectResult(redirectTo);
            }
            else
            {
                var isExists = MenuExist(session.GetString("UserID"), currentAction, httpContextAccessor);
                if (isExists) return;
                string redirectTo = $"~/LoginRegistration/Index";
                context.Result = new RedirectResult(redirectTo);
            }

            base.OnActionExecuting(context);
        }

        private bool MenuExist(string userId, string url, IHttpContextAccessor _httpContextAccessor)
        {
            
            string qry = $"SELECT NODENAME FROM SA_MENU WHERE UserID = '{userId}' AND FORMURL = '{url}'";
            DataTable dt = new SaHelper().DataTableFn(new DataBaseConnection().SAConnStrReader(), qry);
            _httpContextAccessor.HttpContext.Session.SetString("FormNameTitle", "");

            if (dt.Rows.Count > 0)
            {
                _httpContextAccessor.HttpContext.Session.SetString("FormNameTitle", dt.Rows[0]["NODENAME"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}