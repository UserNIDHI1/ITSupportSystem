//using ITSupportSystem.Core1.Models;
//using ITSupportSystem.DataAccess.SQL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ITSupportSystem.WebUI.ActionFilter
//{
//    public class AccessCode
//    {
//        public static bool CheckPermissions(string formaccessCode, string action)
//        {
//            Guid RoleId = (Guid)HttpContext.Current.Session["RoleId"];
//            {
//                using (DataContext dataContext = new DataContext())
//                {
//                    Guid FormId = dataContext.Form.Where(x => x.FormAccessCode == formaccessCode).Select(x => x.Id).FirstOrDefault();
//                    Permission CheckRight = dataContext.Permission.Where(x => x.RoleId == RoleId && x.FormId == x.FormId).FirstOrDefault();

//                    if (CheckRight != null)
//                    {
//                        if ((bool)CheckRight.View == true)
//                        {
//                            CheckPermission.View = CheckRight.View;
//                            CheckPermission.Insert = CheckRight.Insert;
//                            CheckPermission.Update = CheckRight.Update;
//                            CheckPermission.Delete = CheckRight.Delete;

//                            if ((bool)CheckRight.View == true)
//                            {
//                                CheckPermission.View = (bool)CheckRight.View;
//                            }
//                            if ((bool)CheckRight.Insert == true)
//                            {
//                                CheckPermission.Insert = (bool)CheckRight.Insert;
//                            }
//                            if ((bool)CheckRight.Update == true)
//                            {
//                                CheckPermission.Update = (bool)CheckRight.Update;
//                            }
//                            if ((bool)CheckRight.Delete == true)
//                            {
//                                CheckPermission.Delete = (bool)CheckRight.Delete;
//                            }
//                        }
//                        else
//                        {
//                            CheckRight.View = false;
//                            CheckRight.Insert = false;
//                            CheckRight.Update = false;
//                            CheckRight.Delete = false;
//                            return false;
//                        }
//                    }
//                    else
//                    {
//                        CheckRight.View = false;
//                        CheckRight.Insert = false;
//                        CheckRight.Update = false;
//                        CheckRight.Delete = false;
//                        return false;
//                    }                  
//                    return true;
//                }
//            }
//        }
//    }
//}