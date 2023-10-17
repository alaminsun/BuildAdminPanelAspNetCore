using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BuildAdminPanelAspNetCore.Universal
{
    public static class Encryption
    {
        public static string Encrypt(string szPlainText)
        {
            int iCount;
            string szReturn = "";
            foreach (char ch in szPlainText)
            {
                iCount = ((int)ch);
                iCount = iCount + 4;
                szReturn = szReturn + ((char)iCount);
            }
            return szReturn;

        }
        public static string Decrypt(string szEncText)
        {
            int iCount;
            string szReturn = "";
            foreach (char ch in szEncText)
            {
                iCount = ((int)ch);
                iCount = iCount - 4;
                szReturn = szReturn + ((char)iCount);
            }
            return szReturn;

        }
        public static string GetServerPathFromSystem()
        {
            return "~/App_Data/Upload/";
        }
    }
}