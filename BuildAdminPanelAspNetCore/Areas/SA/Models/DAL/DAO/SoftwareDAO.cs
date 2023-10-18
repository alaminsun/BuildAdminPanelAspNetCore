using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Universal;
using System.Data;

namespace BuildAdminPanelAspNetCore.Areas.SA.Models.DAL.DAO
{
    public class SoftwareDAO: ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        public bool SaveUpdate(SoftwareBEL master)
        {
            try
            {
               
                string Qry = "Select MAX(SoftwareID) ID from SA_SOFTWARE";
                master.SoftwareID = (master.SoftwareID == "" || master.SoftwareID == null) ? "0" : master.SoftwareID; 
                var tuple = saHelper.ProcedureExecuteTFn4(dbConn.SAConnStrReader(), Qry, "Sa_Software_SSP", "p_ID", "p_Name", "p_ShortName", "p_IsActive", master.SoftwareID, master.SoftwareName, master.SoftwareShortName, master.IsActive.ToString());
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


        public List<SoftwareBEL> GetSoftwareList()
        {
            string Qry = "SELECT SoftwareID,SoftwareName,SoftwareShortName,IsActive from Sa_Software";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<SoftwareBEL> item;

            item = (from DataRow row in dt.Rows
                    select new SoftwareBEL
                    {
                        SoftwareID = row["SoftwareID"].ToString(),
                        SoftwareName = row["SoftwareName"].ToString(),
                        SoftwareShortName = row["SoftwareShortName"].ToString(), 
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;
        }

        public bool DeleteExecute(SoftwareBEL softwareBEL)
        {
            string Qry = " Delete from Sa_Software Where SoftwareID='" + softwareBEL.SoftwareID + "'";
            if (saHelper.ExecuteFn(dbConn.SAConnStrReader(), Qry))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SoftwareBEL GetSoftwareById(int? id)
        {
            //DataTable companyData = await GetCompanyByIdDataTable(db, id);
            string Qry = "SELECT SoftwareID,SoftwareName,SoftwareShortName,IsActive from Sa_Software Where SoftwareID=" + id + " ";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);

            if (dt != null && dt.Rows.Count > 0)
            {

                SoftwareBEL software = new SoftwareBEL
                {
                    SoftwareID = dt.Rows[0]["SoftwareID"].ToString(),
                    SoftwareName = dt.Rows[0]["SoftwareName"].ToString(),
                    SoftwareShortName = dt.Rows[0]["SoftwareShortName"].ToString(),
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()),
                    //COMPANY_ADDRESS2 = companyData.Rows[0]["COMPANY_ADDRESS2"].ToString()
                };
                return software;

            }
            return null;
        }

    }
}