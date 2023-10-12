using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Systems.Universal
{
    public class SaPermission
    {
        SaHelper saHelper = new SaHelper();
        SaPermisstionServerModel ServerModel = new SaPermisstionServerModel();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SaPermission(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<SaPermissionModel> GetSaP(string ConnString, string funName, string FieldName, string FieldValue)
        {
            DataTable dt = saHelper.DataTableRefCursorFn1(ConnString, funName, FieldName, FieldValue);
            ServerModel.SetPermission(dt);

            List<SaPermissionModel> item;
            item = (from DataRow row in dt.Rows
                    select new SaPermissionModel
                    {
                        ResetPermission = true,
                        ViewPermission = Convert.ToBoolean(row["ViewPermission"].ToString()),
                        SavePermission = Convert.ToBoolean(row["SavePermission"].ToString()),
                        EditPermission = Convert.ToBoolean(row["EditPermission"].ToString()),
                        DeletePermission = Convert.ToBoolean(row["DeletePermission"].ToString()),
                        PrintPermission = Convert.ToBoolean(row["PrintPermission"].ToString()),

                    }).ToList();
            return item;
        }



    }
    public class MenuItemView
    {
        public Int64? MenuID { get; set; }
        public string MenuName { get; set; }
        public Int64? ParentID { get; set; }
        public string FormURL { get; set; }
        public string MenuImage { get; set; }
        //public bool HasChild { get; set; }
        public IList<MenuItemView> MenuItemList { get; set; }

    }
    public class SaOwnerData
    {
        SaOwnerModel saMasterShipModel = new SaOwnerModel();
        private  IHttpContextAccessor httpContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SaOwnerData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public SaOwnerData()
        {
            saMasterShipModel.SetMasterShip(_httpContextAccessor.HttpContext.Session);           
        }
        public string OwnerID = SaOwnerModel.OwnerID;
        public string OwnerName = SaOwnerModel.OwnerName;
        public string OwnerTitle = SaOwnerModel.OwnerTitle;
        public string OwnerContactAddress = SaOwnerModel.OwnerContactAddress;
        public string SolutionPrefix = SaOwnerModel.SolutionPrefix;
        public string SolutionSuffix = SaOwnerModel.SolutionSuffix;
        public string SolutionTitlePrefix = SaOwnerModel.SolutionTitlePrefix;
        public string SolutionTitleSuffix = SaOwnerModel.SolutionTitleSuffix;
        public string VersionNo = SaOwnerModel.VersionNo;
        public string FiscalYear = SaOwnerModel.FiscalYear;
        public string DevBy = SaOwnerModel.DevBy;
        public string DevURL = SaOwnerModel.DevURL;
        public string DevContact = SaOwnerModel.DevContact;


         

    }
    public class SaOwnerModel
    {
        DataBaseConnection dbConnection = new DataBaseConnection();
        SaHelper saHelper = new SaHelper();
        //private IHttpContextAccessor httpContextAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SaOwnerModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public SaOwnerModel()
        {
            SetMasterShip(_httpContextAccessor.HttpContext.Session);
        }

        private static string ownerID;
        private static string ownerName;
        private static string ownerTitle;
        private static string ownerContactAddress;
        private static string solutionPrefix;
        private static string solutionSuffix;
        private static string solutionTitlePrefix;
        private static string solutionTitleSuffix;
        private static string fiscalYear;
        private static string versionNo;
        private static string devURL;
        private static string devContact;
        private static string devBy;
        private static Int64 isRunning;


        public static string OwnerID { get { return ownerID; } }
        public static string OwnerName { get { return ownerName; } }
        public static string OwnerTitle { get { return ownerTitle; } }
        public static string OwnerContactAddress { get { return ownerContactAddress; } }
        public static string SolutionTitlePrefix { get { return solutionTitlePrefix; } }
        public static string SolutionTitleSuffix { get { return solutionTitleSuffix; } }
        public static string SolutionPrefix { get { return solutionPrefix; } }
        public static string SolutionSuffix { get { return solutionSuffix; } }
        public static string VersionNo { get { return versionNo; } }
        public static string FiscalYear { get { return fiscalYear; } }
        public static string DevBy { get { return devBy; } }
        public static string DevURL { get { return devURL; } }
        public static string DevContact { get { return devContact; } }
        public static Int64 IsRunning { get { return isRunning; } }



        //public void SetMasterShip(IHttpContextAccessor httpContextAccessor )
        //{
        //    ownerID = "";
        //    ownerName = "";
        //    ownerTitle = "";
        //    ownerContactAddress = "";
        //    solutionTitlePrefix = "";
        //    devURL = "";
        //    devBy = "";
        //    isRunning = 0;
        //    string Qry = "Select OwnerID,OwnerName,OwnerTitle,OwnerContactAddress,SolutionPrefix,SolutionSuffix,SolutionTitlePrefix,SolutionTitleSuffix,VersionNo,FiscalYear," +
        //    " DevBy,DevContact,DevURL,IsRunning  " +
        //   " from Sa_Owner Where IsRunning=1";

        //    DataTable dt = saHelper.DataTableFn(dbConnection.SAConnStrReader(), Qry);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ownerID = dt.Rows[0]["OwnerID"].ToString();
        //        ownerName = dt.Rows[0]["OwnerName"].ToString();
        //        ownerTitle = dt.Rows[0]["OwnerTitle"].ToString();
        //        ownerContactAddress = dt.Rows[0]["OwnerContactAddress"].ToString();
        //        solutionPrefix = dt.Rows[0]["SolutionPrefix"].ToString();
        //        solutionSuffix = dt.Rows[0]["SolutionSuffix"].ToString();
        //        solutionTitlePrefix = dt.Rows[0]["SolutionTitlePrefix"].ToString();
        //        solutionTitleSuffix = dt.Rows[0]["SolutionTitleSuffix"].ToString();
        //        versionNo = dt.Rows[0]["VersionNo"].ToString();
        //        fiscalYear = dt.Rows[0]["FiscalYear"].ToString();
        //        devURL = dt.Rows[0]["DevURL"].ToString();
        //        devBy = dt.Rows[0]["DevBy"].ToString();
        //        devContact = dt.Rows[0]["DevContact"].ToString();

        //    }

        //    //httpContext.Session.SetString("OwnerName", OwnerName);
        //    httpContextAccessor.HttpContext.Session.SetString("OwnerName", OwnerName);
        //    //httpContextAccessor.HttpContext.Request.Cookies["OwnerName"] = OwnerName;
        //    //httpContext.Session.SetString("OwnerName") = OwnerName;
        //    //httpContext.Session.SetString("SolutionPrefix", SolutionPrefix);
        //    //httpContext.Session.SetString("SolutionSuffix", SolutionSuffix);
        //    //httpContext.Session.SetString("SolutionTitlePrefix", SolutionTitlePrefix);
        //    //httpContext.Session.SetString("SolutionTitleSuffix", SolutionTitleSuffix);
        //    //httpContext.Session.SetString("VersionNo", VersionNo);
        //    //httpContext.Session.SetString("FiscalYear", FiscalYear);
        //    //httpContext.Session.SetString("DevBy", DevBy);
        //    //httpContext.Session.SetString("DevURL", DevURL);
        //    //httpContext.Session.SetString("FormName", "Home");
        //    //httpContext.Session.SetString("Preview", "Preview");

        //    //HttpContext.Current.Session["OwnerName"] = OwnerName;
        //    //HttpContext.Current.Session["SolutionPrefix"] = SolutionPrefix;
        //    //HttpContext.Current.Session["SolutionSuffix"] = SolutionSuffix;
        //    //HttpContext.Current.Session["SolutionTitlePrefix"] = SolutionTitlePrefix;
        //    //HttpContext.Current.Session["SolutionTitleSuffix"] = SolutionTitleSuffix;
        //    //HttpContext.Current.Session["VersionNo"] = VersionNo;
        //    //HttpContext.Current.Session["FiscalYear"] =FiscalYear;
        //    //HttpContext.Current.Session["DevBy"] = DevBy;
        //    //HttpContext.Current.Session["DevURL"] = DevURL;
        //    //HttpContext.Current.Session["FormName"] = "Home";
        //    //HttpContext.Current.Session["Preview"] = "Preview";

        //}


        public void SetMasterShip(ISession session)
        {
            ownerID = "";
            ownerName = "";
            ownerTitle = "";
            ownerContactAddress = "";
            solutionTitlePrefix = "";
            devURL = "";
            devBy = "";
            isRunning = 0;

            string Qry = "Select OwnerID,OwnerName,OwnerTitle,OwnerContactAddress,SolutionPrefix,SolutionSuffix,SolutionTitlePrefix,SolutionTitleSuffix,VersionNo,FiscalYear," +
                         " DevBy,DevContact,DevURL,IsRunning " +
                         " from Sa_Owner Where IsRunning=1";

            DataTable dt = saHelper.DataTableFn(dbConnection.SAConnStrReader(), Qry);

            if (dt.Rows.Count > 0)
            {
                ownerID = dt.Rows[0]["OwnerID"].ToString();
                ownerName = dt.Rows[0]["OwnerName"].ToString();
                ownerTitle = dt.Rows[0]["OwnerTitle"].ToString();
                ownerContactAddress = dt.Rows[0]["OwnerContactAddress"].ToString();
                solutionPrefix = dt.Rows[0]["SolutionPrefix"].ToString();
                solutionSuffix = dt.Rows[0]["SolutionSuffix"].ToString();
                solutionTitlePrefix = dt.Rows[0]["SolutionTitlePrefix"].ToString();
                solutionTitleSuffix = dt.Rows[0]["SolutionTitleSuffix"].ToString();
                versionNo = dt.Rows[0]["VersionNo"].ToString();
                fiscalYear = dt.Rows[0]["FiscalYear"].ToString();
                devURL = dt.Rows[0]["DevURL"].ToString();
                devBy = dt.Rows[0]["DevBy"].ToString();
                devContact = dt.Rows[0]["DevContact"].ToString();

                // Store values in session
                session.SetString("OwnerName", ownerName);
                session.SetString("SolutionPrefix", solutionPrefix);
                session.SetString("SolutionSuffix", solutionSuffix);
                session.SetString("SolutionTitlePrefix", solutionTitlePrefix);
                session.SetString("SolutionTitleSuffix", solutionTitleSuffix);
                session.SetString("VersionNo", versionNo);
                session.SetString("FiscalYear", fiscalYear);
                session.SetString("DevBy", devBy);
                session.SetString("DevURL", devURL);
                session.SetString("FormName", "Home");
                session.SetString("Preview", "Preview");
            }
        }


    }
    public class SaPermissionModel
    {
        public bool ResetPermission { get; set; }
        public bool ViewPermission { get; set; }
        public bool SavePermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
        public bool PrintPermission { get; set; }
        public bool PostingPermission { get; set; }

    }

    public class SaPermisstionServerModel
    {
        public SaPermisstionServerModel()
        {

        }

        private static bool viewPermission = false;
        private static bool savePermission = false;
        private static bool editPermission = false;
        private static bool deletePermission = false;
        private static bool printPermission = false;


        public static bool ViewPermission
        {
            get { return viewPermission; }
        }
        public static bool SavePermission
        {
            get { return savePermission; }
        }

        public static bool EditPermission
        {
            get { return editPermission; }
        }
        public static bool DeletePermission
        {
            get { return deletePermission; }
        }
        public static bool PrintPermission
        {
            get { return printPermission; }
        }


        public bool SetPermission(DataTable dataTable)
        {
            savePermission = false;
            editPermission = false;
            deletePermission = false;
            printPermission = false;

            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows[0]["ViewPermission"].ToString() == "False")
                {
                    return false;
                }
                else
                {
                    viewPermission = true;
                }
                if (dataTable.Rows[0]["SavePermission"].ToString() == "True")
                {
                    savePermission = true;
                }
                if (dataTable.Rows[0]["EditPermission"].ToString() == "True")
                {
                    editPermission = true;
                }
                if (dataTable.Rows[0]["DeletePermission"].ToString() == "True")
                {
                    deletePermission = true;
                }
                if (dataTable.Rows[0]["PrintPermission"].ToString() == "True")
                {
                    printPermission = true;
                }


                return true;
            }
            else
            {
                return false;
            }

        }


    }
}