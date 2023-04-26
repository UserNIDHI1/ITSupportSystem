using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSupportSystem.WebUI.ActionFilter
{
    public class AccessPermission
    {
        public static bool View { get; set; }
        public static bool Insert { get; set; }
        public static bool Update { get; set; }
        public static bool Delete { get; set; }

        public static string IsView = "IsView";
        public static string IsInsert = "IsInsert";
        public static string IsUpdate = "IsUpdate";
        public static string IsDelete = "IsDelete";


        public enum PermissionOrder
        {
            IsView = 1,
            IsInsert = 2,
            IsUpdate = 3,
            IsDelete = 4
        }
    } 
}