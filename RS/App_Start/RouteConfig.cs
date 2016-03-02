using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            checkIfAdminExists();
        }


        private static void checkIfAdminExists()
        {
            using (var cn = new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\Zuzana\Documents\Visual Studio 2015\Projects\RS\RS\App_Data\Database1.mdf';Integrated Security=True"))
            {
                string _sql = @"SELECT [user_id] FROM [dbo].[Users] " +
                       @"WHERE [email] = @u ";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                  .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                  .Value = "admin@admin.sk";
                cn.Open();

                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    _sql = @"INSERT INTO Users(email,first_name,last_name,password,phone_number,roles_id)  " +
                            " values(@e,@a,@b,@p,@d,@c)";
                    cmd = new SqlCommand(_sql, cn);
                    cmd.Parameters
                  .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                  .Value = Helpers.SHA1.Encode("admin1234");
                    cmd.Parameters
                  .Add(new SqlParameter("@e", SqlDbType.NVarChar))
                  .Value = "admin@admin.sk";
                    cmd.Parameters
                  .Add(new SqlParameter("@a", SqlDbType.NVarChar))
                  .Value = "admin";
                    cmd.Parameters
                  .Add(new SqlParameter("@b", SqlDbType.NVarChar))
                  .Value = "admin";
                    cmd.Parameters
                  .Add(new SqlParameter("@c", SqlDbType.NVarChar))
                  .Value = "1";
                    cmd.Parameters
                  .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                  .Value = "0";
                    cmd.ExecuteNonQuery();
                    reader.Dispose();
                    cmd.Dispose();
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
        }
    }
}

