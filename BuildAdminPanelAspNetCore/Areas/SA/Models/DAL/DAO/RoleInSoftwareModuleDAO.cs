

using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Universal;
using NuGet.Protocol.Plugins;
using System.Data;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.DAL.DAO
{
    public class RoleInSoftwareModuleDAO:ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public Boolean SaveUpdate(RoleInSoftwareMasterBEL master)
        {
            bool IsTrue = false;
            if (master != null)
            {
                if (master.DetailList != null)
                {
                    foreach (RoleInSoftwareDetailBEL details in master.DetailList)
                    {
                        IsTrue = false;
                        string Qry = "Select MAX(RoleID) ID from Sa_RoleInSM";
                        var tuple = saHelper.ProcedureExecuteTFn2(dbConn.SAConnStrReader(), Qry, "Sa_RoleIn_SM_SP", "p_RoleSMID", "p_IsActive", master.RoleID + details.SoftwareID + details.ModuleID, details.IsActive.ToString());
                        if (tuple.Item1)
                        {
                            MaxID = tuple.Item2;
                            IUMode = tuple.Item3;
                            IsTrue= true;
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
        public IList<RoleInSoftwareModuleBEL> GetRoleInSoftwareModuleMapping(string RoleID)
        {
            string roleID = _httpContextAccessor.HttpContext.Session.GetString("RoleID");

            if ((RoleID == "" || RoleID == null || RoleID == "null"))
            {
                RoleID = roleID;
            }
            else
            {
                RoleID = (string?)RoleID;
            }
            //string Qry = "Select c.SoftwareID,c.SoftwareName,c.ModuleID,c.ModuleName,ISNULL(d.IsActive,'false') IsActive  from  " +
            //    " (Select a.SoftwareID,a.SoftwareName,b.ModuleID,b.ModuleName from Sa_Software  a,Sa_Module  b Where upper(a.IsActive)=upper('true') and upper(b.IsActive)=upper('true') /* and a.SoftwareID='02' */)  c " +
            //    " LEFT JOIN Sa_RoleInSM  d ON c.SoftwareID+c.ModuleID=d.SoftwareID+d.ModuleID and d.RoleID='" + RoleID + "' Order By c.SoftwareID,c.ModuleID";
            string Qry = "Select c.SoftwareID,c.SoftwareName,c.ModuleID,c.ModuleName,d.IsActive  from   (Select a.SoftwareID,a.SoftwareName,b.ModuleID,b.ModuleName from Sa_Software  a,Sa_Module b Where upper(a.IsActive)=upper('true') and upper(b.IsActive)=upper('true') /* and a.SoftwareID='02' */)  c ,SA_ROLEINSM  d Where c.SoftwareID+c.ModuleID=d.SoftwareID+d.ModuleID and d.RoleID='" + RoleID + "' Order By c.SoftwareID,c.ModuleID";

            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<RoleInSoftwareModuleBEL> items = new List<RoleInSoftwareModuleBEL>();
            var result = dt;
            var length = result.Rows.Count;
            int Sl = 1;
            if (length > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    var model = new RoleInSoftwareModuleBEL();
                    model.SL = Sl;
                    model.SoftwareID = row["SoftwareID"].ToString();
                    model.SoftwareName = row["SoftwareName"].ToString();
                    model.ModuleID = row["ModuleID"].ToString();
                    model.ModuleName = row["ModuleName"].ToString();
                    model.IsActive = Convert.ToBoolean(row["IsActive"].ToString());
                    Sl++;
                    items.Add(model);
                }

            }

            return items;
        }
    }
}