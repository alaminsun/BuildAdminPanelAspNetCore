using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Universal;
using System.Data;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.DAL.DAO
{
    public class RoleInFormDAO : ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public List<RoleInFormBEL> GetRoleInFormPermissionList(string RoleID)
        {
            //RoleID = (RoleID == "" || RoleID == null) ? HttpContext.Current.Session["RoleID"].ToString() : RoleID;
            //string roleID =

            if ((RoleID == "" || RoleID == null || RoleID == "null"))
            {
                RoleID = _httpContextAccessor.HttpContext.Session.GetString("RoleID");
            }
            else
            {
                RoleID = (string?)RoleID;
            }

            string Qry =" Select distinct  p.SoftwareID,p.SoftwareName, p.ModuleID,p.ModuleName, p.FormID,p.FormName,p.FormURL "+
                        " ,ISNULL(e.ViewPermission,'false')  ViewPermission,ISNULL(e.SavePermission,'false')  SavePermission,ISNULL(e.EditPermission,'false') EditPermission,ISNULL(e.DeletePermission,'false') DeletePermission," +
                        " ISNULL(e.PrintPermission,'false')  PrintPermission,e.SetOn" +
                        " from ("+
                        " (Select  a.RoleID,a.SoftwareID,d.SoftwareName, a.ModuleID,c.ModuleName, b.FormID,b.FormName,b.FormURL from Sa_RoleInSM a,Sa_Form  b,Sa_Module  c,Sa_Software d  "+
                        " Where upper(a.IsActive)=upper('true') and a.RoleID='" + RoleID + "' and a.SoftwareID+a.ModuleID+b.FormID=d.SoftwareID+c.ModuleID+b.FormID and d.SoftwareID='02'and a.ModuleID='01' and b.FormID  between '100' and '149') "+
                        " Union all " +
                         " (Select  a.RoleID,a.SoftwareID,d.SoftwareName, a.ModuleID,c.ModuleName, b.FormID,b.FormName,b.FormURL from Sa_RoleInSM a,Sa_Form  b,Sa_Module  c,Sa_Software d  " +
                        " Where upper(a.IsActive)=upper('true') and a.RoleID='" + RoleID + "' and a.SoftwareID+a.ModuleID+b.FormID=d.SoftwareID+c.ModuleID+b.FormID and d.SoftwareID='02'and a.ModuleID='02' and b.FormID  between '150' and '179') " +
                        " Union all " +
                        " (Select  a.RoleID,a.SoftwareID,d.SoftwareName, a.ModuleID,c.ModuleName, b.FormID,b.FormName,b.FormURL  from Sa_RoleInSM a,Sa_Form  b,Sa_Module  c, Sa_Software d "+
                        " Where upper(a.IsActive)=upper('true') and a.RoleID='" + RoleID + "' and a.SoftwareID+a.ModuleID+b.FormID=d.SoftwareID+c.ModuleID+b.FormID  and d.SoftwareID='01'and a.ModuleID='01' and b.FormID  between '001' and '049') " +
                        " Union all " +
                        " (Select  a.RoleID,a.SoftwareID,d.SoftwareName, a.ModuleID,c.ModuleName, b.FormID,b.FormName,b.FormURL  from Sa_RoleInSM a,Sa_Form  b,Sa_Module  c, Sa_Software d " +
                        " Where upper(a.IsActive)=upper('true') and a.RoleID='" + RoleID + "' and a.SoftwareID+a.ModuleID+b.FormID=d.SoftwareID+c.ModuleID+b.FormID  and d.SoftwareID='01'and a.ModuleID='02' and b.FormID  between '050' and '079') " +
                        " )  p " +
                        " LEFT JOIN Sa_RoleInFormP  e ON p.RoleID+p.SoftwareID+p.ModuleID+p.FormID=e.RoleID+e.SoftwareID+e.ModuleID+e.FormID   "+
                        " Order by p.SoftwareID";

            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<RoleInFormBEL> items = new List<RoleInFormBEL>();
            var result = dt;
            var length = result.Rows.Count;
            int Sl = 0;
            if (length > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    var model = new RoleInFormBEL();
                    model.SL = Sl;
                    model.SoftwareID = row["SoftwareID"].ToString();
                    model.SoftwareName = row["SoftwareName"].ToString();
                    model.ModuleID = row["ModuleID"].ToString();
                    model.ModuleName = row["ModuleName"].ToString();
                    model.FormID = row["FormID"].ToString();
                    model.FormName = row["FormName"].ToString();
                    model.FormURL = row["FormURL"].ToString();
                    model.ViewPermission = Convert.ToBoolean(row["ViewPermission"].ToString());
                    model.SavePermission = Convert.ToBoolean(row["SavePermission"].ToString());
                    model.EditPermission = Convert.ToBoolean(row["EditPermission"].ToString());
                    model.DeletePermission = Convert.ToBoolean(row["DeletePermission"].ToString());
                    model.PrintPermission = Convert.ToBoolean(row["PrintPermission"].ToString());
                    Sl++;
                    items.Add(model);
                }

            }

            return items;
        }

        public bool SaveUpdate(RoleInFormMaster master)
        {
            bool IsTrue = false;
            if (master != null)
            {
                if (master.DetailList != null)
                {
                    foreach (RoleInFormDetail details in master.DetailList)
                    {
                        IsTrue = false;
                        string VSEDP = Convert.ToString(details.ViewPermission == true ? 1 : 0) + Convert.ToString(details.SavePermission == true ? 1 : 0) + Convert.ToString(details.EditPermission == true ? 1 : 0) + Convert.ToString(details.DeletePermission == true ? 1 : 0) + Convert.ToString(details.PrintPermission == true ? 1 : 0);
                        //string UserID = HttpContext.Current.Session["UserID"].ToString();
                        string UserID = _httpContextAccessor.HttpContext.Session.GetString("UserID");
                        string Qry = "Select MAX(RoleID) ID from Sa_RoleInFormP";
                        var tuple = saHelper.ProcedureExecuteTFn3(dbConn.SAConnStrReader(), Qry, "Sa_RoleIn_SMP_SP", "P_RSMFormID", "p_VSEDP", "p_SetOn", master.RoleID + details.SoftwareID + details.ModuleID + details.FormID, VSEDP, UserID);
                        if (tuple.Item1)
                        {
                            MaxID = tuple.Item2;
                            IUMode = tuple.Item3;
                            
                            if (saHelper.ProcedureExecuteFn1(dbConn.SAConnStrReader(), "", "Sa_Menu_SP", "pUserID", UserID))
                            {
                                IsTrue = true;
                            }
                        }
                        else
                        {
                            IsTrue = false;
                        }
                    }
                }
            }
            return IsTrue;
        }

    }
}