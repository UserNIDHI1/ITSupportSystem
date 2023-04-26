using ITSupportSystem.Core1.Models;
using ITSupportSystem.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITSupportSystem.WebUI.ActionFilter
{
    public class AccessCode
    {
        public static bool CheckAccess(string formaccessCode, string action)
        {
            Guid currentRoleId = (Guid)HttpContext.Current.Session["RoleId"];
            {
                using (DataContext dataContext = new DataContext())
                {
                    Guid FormId = dataContext.Form
                                    .Where(x => x.FormAccessCode == formaccessCode)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();
                    Permission CheckPermission = dataContext.Permission
                                    .Where(x => x.RoleId == currentRoleId && x.FormId == FormId)
                                    .FirstOrDefault();

                    if (CheckPermission != null)
                    {
                        if ((bool)CheckPermission.View == true)
                        {
                            AccessPermission.View = (bool)CheckPermission.View;
                            AccessPermission.Insert = (bool)CheckPermission.Insert;
                            AccessPermission.Update = (bool)CheckPermission.Update;
                            AccessPermission.Delete = (bool)CheckPermission.Delete;

                            if (action == AccessPermission.PermissionOrder.IsView.ToString())
                            {
                                return (bool)CheckPermission.View; //Retun true or false
                            }
                            if (action == AccessPermission.PermissionOrder.IsInsert.ToString())
                            {
                                return (bool)CheckPermission.Insert;
                            }
                            if (action == AccessPermission.PermissionOrder.IsUpdate.ToString())
                            {
                                return (bool)CheckPermission.Update;
                            }
                            if (action == AccessPermission.PermissionOrder.IsDelete.ToString())
                            {
                                return (bool)CheckPermission.Delete;
                            }
                        }
                        else
                        {
                            AccessPermission.View = false;
                            AccessPermission.Insert = false;
                            AccessPermission.Update = false;
                            AccessPermission.Delete = false;
                            return false;
                        }
                        return false;
                    }
                    else
                    {  
                        AccessPermission.View = false;
                        AccessPermission.Insert = false;
                        AccessPermission.Update = false;
                        AccessPermission.Delete = false;
                        return false;
                    }
                }

            }
        }
        public class PermissionOrder
        {

        }
    }
}