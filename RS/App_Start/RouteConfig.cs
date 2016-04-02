using RS.Models;
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
            
            Database1Entities4 db =  db = new Database1Entities4();
           
            using (var cn = new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\Zuzka\Documents\Visual Studio 2015\Projects\RS\RS\App_Data\Database1.mdf';Integrated Security=True"))
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
                    _sql = @"INSERT INTO Users(email,first_name,last_name,password,phone_number)  " +
                            " values(@e,@a,@b,@p,@d)";
                 string _sql2 = @"INSERT INTO UsersRoles(user_id,roles_id)  " +
                            " values(@u,@r)";
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
                  .Add(new SqlParameter("@d", SqlDbType.NVarChar))
                  .Value = "0";
                    
                    cmd.ExecuteNonQuery();
                    reader.Dispose();


                    string _sql3 = @"SELECT * FROM Users WHERE first_name = 'admin'";
                    cmd = new SqlCommand(_sql3, cn);
                    int user_id = Int32.Parse(cmd.ExecuteScalar().ToString());
                   

                    //    int user_id = Int32.Parse(reader[0].ToString());
                        cmd = new SqlCommand(_sql2, cn);
                        cmd.Parameters
                      .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                      .Value = user_id;
                        //1= admin v Roles
                        cmd.Parameters
                      .Add(new SqlParameter("@r", SqlDbType.NVarChar))
                      .Value = 1;

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

