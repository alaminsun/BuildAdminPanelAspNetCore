

using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Universal;
using System.Data;

namespace RBuildAdminPanelAspNetCore.Models.DAL.DAO
{
    public class RoleDAO :ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //public RoleDAO(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        public Boolean SaveUpdate(RoleBEL roleBEL)
        {
            try
            {
                string Qry = "Select MAX(RoleID) ID from Sa_Role";
                roleBEL.RoleID = (roleBEL.RoleID == "" || roleBEL.RoleID == null)? "0" : roleBEL.RoleID;
                var tuple = saHelper.ProcedureExecuteTFn3(dbConn.SAConnStrReader(), Qry, "Sa_Role_SSP", "p_RoleID", "p_RoleName", "p_IsActive", roleBEL.RoleID, roleBEL.RoleName, roleBEL.IsActive.ToString());

                if (tuple.Item1)
                {
                    MaxID = tuple.Item2;
                    IUMode = tuple.Item3;
                    return true;

                }                                       
                else
                {
                    return false;
                }

            }
            catch (Exception errorException)
            {
                throw errorException;
            }
        }
        
        public bool DeleteExecute(RoleBEL roleBEL)
        {
            string Qry = " Delete from Sa_Role Where RoleID='" + roleBEL.RoleID + "'";
            if (saHelper.ExecuteFn(dbConn.SAConnStrReader(), Qry))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public List<RoleBEL> GetRoleList()
        {
            string Qry = "Select RoleID,RoleName,IsActive From Sa_Role Where RoleID >='" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "'";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<RoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new RoleBEL
                    {
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),
                        IsActive =Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;
        }

        public RoleBEL GetRoleById(int? id)
        {
            string Qry = "Select RoleID,RoleName,IsActive From Sa_Role Where RoleID >='" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "' and RoleID = "+id+"";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);

            if (dt != null && dt.Rows.Count > 0)
            {
                RoleBEL role = new RoleBEL
                {
                    RoleID = dt.Rows[0]["RoleID"].ToString(),
                    RoleName = dt.Rows[0]["RoleName"].ToString(),
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()),

                };

                return role;
            }

            return null;
        }

        public List<RoleBEL> GetRoleInSoftwareModuleMappingList()
        {
            string Qry = "Select distinct a.RoleID,b.RoleName from Sa_RoleInSM a,Sa_Role b Where a.RoleID=b.RoleID and upper(a.IsActive)=upper('true') and a.RoleID >='" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "' Order by a.RoleID";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<RoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new RoleBEL
                    {
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),                        

                    }).ToList();
            return item;
        }
    }
}