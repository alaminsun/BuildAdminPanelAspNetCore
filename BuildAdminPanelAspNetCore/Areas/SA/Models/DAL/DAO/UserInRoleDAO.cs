
using BuildAdminPanelAspNetCore.Areas.SA.Models.BEL;
using BuildAdminPanelAspNetCore.Areas.SA.Models.ViewModels;
using BuildAdminPanelAspNetCore.Universal;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildAdminPanelAspNetCore.Models.DAL.DAO;
using System.Data;
using System.Diagnostics.Metrics;
using System.Text;

namespace BuildAdminPanelAspNetCore.Models.DAL.DAO
{
    public class UserInRoleDAO : ReturnData
    {
        DataBaseConnection dbConn = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        IHttpContextAccessor _httpContextAccessor = new HttpContextAccessor();
        RoleDAO roleDAO = new RoleDAO();

        public bool SaveUpdate(UserInRoleBEL master)
        {
            try
            {
                string Qry = "Select MAX(UserID) ID from Sa_UserInRole";
                var tuple = saHelper.ProcedureExecuteTFn5(dbConn.SAConnStrReader(), Qry, "Sa_UserInRole_SSP", "p_RoleID", "p_UserID", "p_EmpID", "p_NewPassword", "p_IsActive", master.RoleID, Encryption.Encrypt(master.UserID),Convert.ToString(master.EmpID), Encryption.Encrypt(master.Password), master.IsActive.ToString());
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

        public List<UserInRoleBEL> GetUserInRoleList()
        {
            //string Qry = "SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,GetName(ur.EmpID,'EM') EmpName,u.NewPassword,u.OldPassword,ur.IsActive FROM Sa_UserInRole ur, Sa_UserCredential u,Sa_Role r Where ur.UserID=u.UserID and ur.RoleID=r.RoleID and ur.RoleID>='" + HttpContext.Current.Session["RoleID"].ToString() + "'";
            var query = new StringBuilder();
            query.Append(" SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,EI.EMPLOYEE_NAME EmpName,EI.EMPLOYEE_CODE EmpCode,u.NewPassword,u.OldPassword,ur.IsActive  FROM Sa_UserCredential U");
            query.Append(" LEFT JOIN  Sa_UserInRole ur ON U.USERID=ur.USERID");
            query.Append(" LEFT JOIN Sa_Role r ON r.RoleID=ur.RoleID ");
            query.Append(" LEFT JOIN EMPLOYEE_INFO EI ON EI.ID=UR.EMPID ");
            query.Append(" WHERE ur.RoleID >='"+ _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "'");


            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), query.ToString());
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),
                        UserID = Encryption.Decrypt(row["UserID"].ToString()),                     
                        EmpID =  Convert.ToInt32(row["EmpID"]),
                        EmpCode = row["EmpCode"].ToString(),
                        EmpName = row["EmpName"].ToString(),
                        Password = Encryption.Decrypt(row["NewPassword"].ToString()),                        
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();
            return item;
        }

        public List<UserInRoleBEL> GetEmployeeList()
        {
            string Qry = "Select EMPLOYEE_CODE,EMPLOYEE_NAME From Sa_Employee -- where Upper(STATUS)=Upper('true')";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        EmpID =  Convert.ToInt32(row["EmpID"].ToString()),
                        EmpName = row["EMPLOYEE_NAME"].ToString()
                       

                    }).ToList();
            return item;
        }
        public IEnumerable<UserInRoleBEL> GetEmployeeNotYetAssignedList(string roleId)
        {
            string Qry = string.Empty;
            //string Qry = "Select E.ID EmpID,E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID where UR.EmpID is null  ORDER BY E.EMPLOYEE_NAME --and  Upper(STATUS)=Upper('true')";
            //var ID = _httpContextAccessor.HttpContext.Session.GetString("RoleID");
            //if (ID == "01")
            //{
            //    Qry = "Select ID EmpID,EMPLOYEE_CODE,EMPLOYEE_NAME From EMPLOYEE_INFO";
            //    //Qry = "Select E.ID EmpID,E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.EMPID Not in(SELECT EMPID FROM Sa_UserInRole ORDER BY E.EMPLOYEE_NAME";
            //}
            if(string.IsNullOrEmpty(roleId))
            {
                // Qry = "Select E.ID EmpID,E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.EMPID Not in(SELECT EMPID FROM Sa_UserInRole WHERE EMPID =" + _httpContextAccessor.HttpContext.Session.GetString("EmpID") + ") ORDER BY E.EMPLOYEE_NAME";
                //Qry = "Select E.ID EmpID,Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.RoleID Not in(SELECT ROLEID FROM Sa_UserInRole Where ROLEID ='" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "' AND RoleID is null) ORDER BY E.EMPLOYEE_NAME";
                Qry = "Select E.ID EmpID, Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID = E.ID " +
                      " Where ur.USERID Not in(SELECT USERID FROM Sa_UserInRole Where ROLEID = '" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "') or ROLEID is null ORDER BY E.EMPLOYEE_NAME";
            }
            else
            {
                //Qry = "Select E.ID EmpID,Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.RoleID Not in(SELECT ROLEID FROM Sa_UserInRole Where ROLEID ='"+roleId+ "' AND RoleID is null) ORDER BY E.EMPLOYEE_NAME";
                Qry = "Select E.ID EmpID, Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID = E.ID " +
                      " Where ur.USERID Not in(SELECT USERID FROM Sa_UserInRole Where ROLEID = '"+roleId+ "') or ROLEID is null ORDER BY E.EMPLOYEE_NAME";
            }
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        EmpID = Convert.ToInt32(row["EmpID"].ToString()),
                        EmpCode = row["EMPLOYEE_CODE"].ToString(),
                        EmpName = row["EMPLOYEE_NAME"].ToString()
                    }).ToList();
            return item;
        }

        public IEnumerable<UserInRoleBEL> GetEmployeeYetAssignedList()
        {
            string Qry = string.Empty;
            //string Qry = "Select E.ID EmpID,E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID where UR.EmpID is null  ORDER BY E.EMPLOYEE_NAME --and  Upper(STATUS)=Upper('true')";
            //var ID = _httpContextAccessor.HttpContext.Session.GetString("RoleID");
            //if (ID == "01")
            //{
            //    Qry = "Select ID EmpID,EMPLOYEE_CODE,EMPLOYEE_NAME From EMPLOYEE_INFO";
            //    //Qry = "Select E.ID EmpID,E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.EMPID Not in(SELECT EMPID FROM Sa_UserInRole ORDER BY E.EMPLOYEE_NAME";
            ////}
            //if (string.IsNullOrEmpty(roleId))
            //{
            //    // Qry = "Select E.ID EmpID,E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.EMPID Not in(SELECT EMPID FROM Sa_UserInRole WHERE EMPID =" + _httpContextAccessor.HttpContext.Session.GetString("EmpID") + ") ORDER BY E.EMPLOYEE_NAME";
            //    //Qry = "Select E.ID EmpID,Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.RoleID Not in(SELECT ROLEID FROM Sa_UserInRole Where ROLEID ='" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "' AND RoleID is null) ORDER BY E.EMPLOYEE_NAME";
            //    Qry = "Select E.ID EmpID, Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID = E.ID " +
            //          " Where ur.USERID Not in(SELECT USERID FROM Sa_UserInRole Where ROLEID = '" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "') or ROLEID is null ORDER BY E.EMPLOYEE_NAME";
            //}
            //else
            //{
            //    //Qry = "Select E.ID EmpID,Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID=E.ID Where ur.RoleID Not in(SELECT ROLEID FROM Sa_UserInRole Where ROLEID ='"+roleId+ "' AND RoleID is null) ORDER BY E.EMPLOYEE_NAME";
            //    //Qry = "Select E.ID EmpID, Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID = E.ID " +
            //    //      " Where ur.USERID in(SELECT USERID FROM Sa_UserInRole Where ROLEID = '" + roleId + "') or ROLEID is null ORDER BY E.EMPLOYEE_NAME";
            //    Qry = "Select E.ID EmpID, Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID = E.ID " +
            //          " Where ur.USERID in(SELECT USERID FROM Sa_UserInRole) or ROLEID is null ORDER BY E.EMPLOYEE_NAME";
            //}
            Qry = "Select E.ID EmpID, Ur.RoleID, E.EMPLOYEE_CODE,E.EMPLOYEE_NAME From EMPLOYEE_INFO E LEFT JOIN Sa_UserInRole UR ON UR.EmpID = E.ID " +
                     " Where ur.USERID in(SELECT USERID FROM Sa_UserInRole) or ROLEID is null ORDER BY E.EMPLOYEE_NAME";
           DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        EmpID = Convert.ToInt32(row["EmpID"].ToString()),
                        EmpCode = row["EMPLOYEE_CODE"].ToString(),
                        EmpName = row["EMPLOYEE_NAME"].ToString()
                    }).ToList();
            return item;
        }

        //public List<UserInRoleBEL> GetBuyerList()
        //{
        //    string Qry = "Select BUYER_CODE,BUYER_NAME From BUYER_INFO ";
        //    DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
        //    List<UserInRoleBEL> item;

        //    item = (from DataRow row in dt.Rows
        //            select new UserInRoleBEL
        //            {
        //                BuyerID = row["BUYER_CODE"].ToString(),
        //                BuyerName = row["BUYER_NAME"].ToString()


        //            }).ToList();
        //    return item;
        //}

        public List<UserInRoleBEL> GetRoleList(string id)
        {
            string Qry = "Select Ur.RoleID,r.RoleName,Ur.IsActive From Sa_UserInRole Ur LEFT JOIN Sa_Role r ON r.RoleID = ur.RoleID Where  UserID='" + Encryption.Encrypt(id) + "' And Ur.ISACTIVE = 'true'";

            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        RoleID = row["RoleID"].ToString(),
                        RoleName = row["RoleName"].ToString(),
                        IsActive = Convert.ToBoolean(row["IsActive"].ToString())

                    }).ToList();

            return item;
        }
        public List<UserInRoleBEL> GetUserList()
        {
            string Qry = "Select UserID,EmpID,GetName(EmpID,'EM') EmpName From Sa_UserInRole Where Upper(IsActive)=Upper('true')";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
            List<UserInRoleBEL> item;

            item = (from DataRow row in dt.Rows
                    select new UserInRoleBEL
                    {
                        UserID = Encryption.Decrypt(row["UserID"].ToString()),
                        EmpName = row["EmpName"].ToString()
                    }).ToList();
            return item;
        }

        public UserInRoleBEL GetUserInRoleById(string id)
        {
            //string Qry = "Select UserID,EmpID,GetName(EmpID,'EM') EmpName From Sa_UserInRole Where Upper(IsActive)=Upper('true') And UserID ";
            //string Qry = "SELECT FormID,FormName,FormURL,MENUIMAGE,IsActive FROM Sa_Form Where FormID=" + id + " ";
            var query = new StringBuilder();
            query.Append(" SELECT ur.UserID,ur.RoleID,r.RoleName,ur.EmpID,EI.EMPLOYEE_NAME EmpName,EI.EMPLOYEE_CODE EmpCode,u.NewPassword,u.OldPassword,ur.IsActive  FROM Sa_UserCredential U");
            query.Append(" LEFT JOIN  Sa_UserInRole ur ON U.USERID=ur.USERID");
            query.Append(" LEFT JOIN Sa_Role r ON r.RoleID=ur.RoleID ");
            query.Append(" LEFT JOIN EMPLOYEE_INFO EI ON EI.ID=UR.EMPID ");
            //query.Append(" WHERE ur.RoleID >='" + _httpContextAccessor.HttpContext.Session.GetString("RoleID") + "'");
            query.Append(" WHERE ur.USERID ='" + Encryption.Encrypt(id) + "'");
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), query.ToString());
            var roleList = GetRoleList(id).Select(c => new SelectListItem
            {
                Value = c.RoleID.ToString(),
                Text = c.RoleName
            }).ToList();

            //var roleID = GetRoleIdByUser(Encryption.Encrypt(id));

            var empList = GetEmployeeYetAssignedList().Select(c => new SelectListItem
            {
                Value = c.EmpID.ToString(),
                Text = c.EmpName
            }).ToList();

            if (dt != null && dt.Rows.Count > 0)
            {

                UserInRoleBEL userInRole = new UserInRoleBEL
                {
                    RoleID = dt.Rows[0]["RoleID"].ToString(),
                    RoleName = dt.Rows[0]["RoleName"].ToString(),
                    UserID = Encryption.Decrypt(dt.Rows[0]["UserID"].ToString()),
                    EmpID = Convert.ToInt32(dt.Rows[0]["EmpID"]),
                    EmpCode = dt.Rows[0]["EmpCode"].ToString(),
                    EmpName = dt.Rows[0]["EmpName"].ToString(),
                    Password = Encryption.Decrypt(dt.Rows[0]["NewPassword"].ToString()),
                    IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString()),
                    RoleList = roleList,
                    EmpList = empList
                    //COMPANY_ADDRESS2 = companyData.Rows[0]["COMPANY_ADDRESS2"].ToString()
                };
                return userInRole;

            }
            return null;
        }

        private string GetRoleIdByUser(string id)
        {
           string roleID= string.Empty;
            var query = "Select Ur.RoleID,r.RoleName,Ur.IsActive From Sa_UserInRole Ur LEFT JOIN Sa_Role r ON r.RoleID = ur.RoleID Where  UserID='" + id + "' And Ur.ISACTIVE = 'true'";
            DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), query);

            if (dt.Rows.Count > 0)
            {
                roleID = dt.Rows[0][0].ToString();
            }

            return roleID;
        }

        //public List<UserInRoleBEL> GetBuyerYetAssignedList(string EmpID)
        //{
        //    string Qry = "Select BUYER_ID,GetName(BUYER_ID,'BR') BuyerName  From SA_EMP_BUYER_MAPPING where Emp_ID='" + EmpID + "'";
        //    DataTable dt = saHelper.DataTableFn(dbConn.SAConnStrReader(), Qry);
        //    List<UserInRoleBEL> item;

        //    item = (from DataRow row in dt.Rows
        //            select new UserInRoleBEL
        //            {
        //                BuyerID = row["BUYER_ID"].ToString(),
        //                BuyerName = row["BuyerName"].ToString()
        //            }).ToList();
        //    return item;
        //}
    }
}