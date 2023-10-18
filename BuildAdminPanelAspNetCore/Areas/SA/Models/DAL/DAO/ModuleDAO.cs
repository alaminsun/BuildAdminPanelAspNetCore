using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Universal;
using System.Data;

namespace BuildAdminPanelAspNetCore.Models.DAL.DAO
{
    public class ModuleDAO:ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        public Boolean SaveUpdate(ModuleBEL moduleBEL)
        {
            try
            {
                string Qry = "Select MAX(ModuleID) ID from Sa_Module";
                moduleBEL.ModuleID = (moduleBEL.ModuleID == "" || moduleBEL.ModuleID == null) ? "0" : moduleBEL.ModuleID; 
                var tuple = saHelper.ProcedureExecuteTFn3(dbConn.SAConnStrReader(), Qry, "Sa_Module_SSP", "p_ID", "p_Name", "p_IsActive", moduleBEL.ModuleID, moduleBEL.ModuleName, moduleBEL.IsActive.ToString());
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

        

        public bool DeleteExecute(ModuleBEL moduleBEL)
        {
            string Qry = " Delete from Sa_Module Where ModuleID='" + moduleBEL.ModuleID + "'";
            if (saHelper.ExecuteFn(dbConn.SAConnStrReader(), Qry))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<ModuleBEL> GetModuleList()
        {
            string Qry = "Select ModuleID,ModuleName,IsActive From Sa_Module ";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<ModuleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new ModuleBEL
                    {
                        ModuleID = row["ModuleID"].ToString(),
                        ModuleName = row["ModuleName"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;
        }

        public ModuleBEL GetModuleById(int? id)
        {
            //DataTable companyData = await GetCompanyByIdDataTable(db, id);
            string Qry = "Select ModuleID,ModuleName,IsActive From Sa_Module Where ModuleID=" + id + " ";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);

            if (dt != null && dt.Rows.Count > 0)
            {

                ModuleBEL module = new ModuleBEL
                {
                    ModuleID = dt.Rows[0]["ModuleID"].ToString(),
                    ModuleName = dt.Rows[0]["ModuleName"].ToString(),
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()),
                    //COMPANY_ADDRESS2 = companyData.Rows[0]["COMPANY_ADDRESS2"].ToString()
                };
                return module;

            }
            return null;
        }
    }
}