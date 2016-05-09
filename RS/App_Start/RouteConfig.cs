using RS.Models;
using RS.Service;
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

            Services.Instance.Notify(SQLConnection.Instance.Database);
            checkIfAdminExists();
        }

        private static void checkIfAdminExists()
        {
            bool found = false;
            foreach (Users u in SQLConnection.Instance.Database.Users)
            {
                if (u.email.Equals("admin@admin.sk"))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                Users admin = new Users();
                admin.email = "admin@admin.sk";
                admin.password = Helpers.SHA1.Encode("admin1234");
                admin.ConfirmPassword = admin.password;
                admin.first_name = "admin";
                admin.last_name = "admin";
                admin.phone_number = "+421 900 000 000";

                SQLConnection.Instance.Database.Users.Add(admin);
                SQLConnection.Instance.Database.SaveChanges();

                UsersRoles usersRoles = new UsersRoles
                {
                    user_id = admin.user_id,
                    roles_id = 1
                };

                SQLConnection.Instance.Database.UsersRoles.Add(usersRoles);
                SQLConnection.Instance.Database.SaveChanges();
            }
        }
    }
}
