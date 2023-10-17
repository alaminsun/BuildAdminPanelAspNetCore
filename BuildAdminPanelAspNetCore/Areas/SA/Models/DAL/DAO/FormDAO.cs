
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Universal;
using System.Data;

namespace BuildAdminPanelAspNetCore.Models.DAL.DAO
{
    public class FormDAO:ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        //DBHelper saHelper = new DBHelper();
        public Boolean SaveUpdate(FormBEL formBEL)
        {
            try
            {
                string Qry = "Select MAX(FormID) ID from Sa_Form";
                formBEL.FormID = (formBEL.FormID == "" || formBEL.FormID == null) ? "0" : formBEL.FormID;  
                var tuple = saHelper.ProcedureExecuteTFn4(dbConn.SAConnStrReader(), Qry, "Sa_Form_SSP", "p_ID", "p_Name", "p_FormURL", "p_IsActive", formBEL.FormID, formBEL.FormName, formBEL.FormURL, formBEL.IsActive.ToString());
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



        public bool DeleteExecute(FormBEL formBEL)
        {
            string Qry = " Delete from Sa_Form Where FormID='" + formBEL.FormID + "'";
            if (saHelper.ExecuteFn(dbConn.SAConnStrReader(),Qry))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<FormBEL> GetFormList()
        {
            string Qry = "SELECT FormID,FormName,FormURL,IsActive FROM Sa_Form ";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(),Qry);
            List<FormBEL> item;

            item = (from DataRow row in dt.Rows
                    select new FormBEL
                    {
                        FormID = row["FormID"].ToString(),
                        FormName = row["FormName"].ToString(),
                        FormURL = row["FormURL"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;
        }

        //public FormBEL GetForm(int? id)
        //{
            

        //    var fromdto = new FormBEL { 
        //        FormID = id.ToString(),
        //        FormName = row["FormID"].ToString(),
        //    }
            

        //}

        public FormBEL GetForm(int? id)
        {
            //DataTable companyData = await GetCompanyByIdDataTable(db, id);
            string Qry = "SELECT FormID,FormName,FormURL,IsActive FROM Sa_Form Where FormID=" + id + " ";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);

            if (dt != null && dt.Rows.Count > 0)
            {

                FormBEL company = new FormBEL
                {
                    FormID = dt.Rows[0]["FormID"].ToString(),
                    FormName = dt.Rows[0]["FormName"].ToString(),
                    FormURL = dt.Rows[0]["FormURL"].ToString(),
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()),
                    //COMPANY_ADDRESS2 = companyData.Rows[0]["COMPANY_ADDRESS2"].ToString()
                };
                return company;

            }
            return null;
        }


    }
}