using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UpExcise.Models;

namespace UpExcise.Repository
{
    public class MenuData
    {
        public static IList<Menu> GetMenus(string UserId, int Role)
        {

            /* using ado.net code */
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                List<Menu> menuList = new List<Menu>();
                SqlCommand cmd = new SqlCommand("usp_GetMenuData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Role", Role);
                cmd.Parameters.AddWithValue("@Sptype", 1);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Menu menu = new Menu();
                    menu.MID = Convert.ToInt32(sdr["MenuId"].ToString());
                    menu.MenuName = sdr["MenuName"].ToString();
                    menu.MenuURL = sdr["RoutingUrl"].ToString();
                    menu.MenuParentID = Convert.ToInt32(sdr["MenuParentID"].ToString());
                    menu.icon = sdr["icon"].ToString();
                    menu.badge = sdr["badge"].ToString();
                    menuList.Add(menu);
                }
                return menuList;
            }

            /* use can also use dapper orm
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ToString()))
            {
                try
                {
                    var para = new DynamicParameters();
                    para.Add("@UserId", UserId);
                    var MenuList = con.Query<Menu>("usp_GetMenuData", para, null, true, 0, CommandType.StoredProcedure);
                    return MenuList.ToList();
                }
                catch
                {
                    return null;
                }
            }*/
        }
        public static int GetMenuValid(string userid, string pagename, int RoleId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@userid", userid);
                para.Add("@PageNev", pagename);
                para.Add("@role", RoleId);
                para.Add("@Sptype", 2);
                para.Add("@pOutPut", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("usp_GetMenuData", para, null, 0, CommandType.StoredProcedure);
                //return con.Query<BOLogin>("Usp_Userlogin", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();


                int MemID = para.Get<int>("@pOutPut");
                return MemID;
            }


        }




        internal static void InsertErrLog(string PageNev, string ErrMsg)
        {
            //// string Errid = objg.GetNewID_String("Err_Log", "ErrID", 8);

            //string sql = "Insert into tbl_errorlog(IP,ErrPage,ErrLog) values(@IP,@ErrPage,@ErrLog)";
            //SqlParameter[] sqlparam = {
            //    new SqlParameter("@IP", IP),
            //    new SqlParameter("@ErrPage", UrlPage),
            //    new SqlParameter("@ErrLog", ErrMsg)
            //};

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {

                string IP = Fetch_UserIP();
                var para = new DynamicParameters();
                para.Add("@PageNev", PageNev);
                para.Add("@ErrMsg", ErrMsg);
                para.Add("@IP", IP);
                para.Add("@Sptype", 3);
                //  para.Add("@pOutPut", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("usp_GetMenuData", para, null, 0, CommandType.StoredProcedure);
                //return con.Query<BOLogin>("Usp_Userlogin", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();


                //int MemID = para.Get<int>("@pOutPut");
                //return MemID;
            }
        }


        private static string Fetch_UserIP()
        {
            string VisitorsIPAddress = string.Empty;
            try
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    VisitorsIPAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (System.Web.HttpContext.Current.Request.UserHostAddress.Length != 0)
                {
                    VisitorsIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
            }
            catch (Exception ex)
            {

                //Handle Exceptions  
            }
            return VisitorsIPAddress;
        }


        internal static void InsertAudit(AuditTB objaudit)
        {


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                // string IP = Fetch_UserIP();
                var para = new DynamicParameters();
                para.Add("@ActionName", objaudit.ActionName);
                para.Add("@ControllerName", objaudit.ControllerName);
                para.Add("@IPAddress", objaudit.IPAddress);
                para.Add("@LoggedInAt", objaudit.LoggedInAt);
                para.Add("@LoggedOutAt", objaudit.LoggedOutAt);
                para.Add("@LoginStatus", objaudit.LoginStatus);
                para.Add("@PageAccessed", objaudit.PageAccessed);
                para.Add("@SessionID", objaudit.SessionID);
                para.Add("@UserID", objaudit.UserID);
                para.Add("@UsersAuditID", objaudit.UsersAuditID);
                con.Execute("usp_Audit", para, null, 0, CommandType.StoredProcedure);

            }
        }




    }
}