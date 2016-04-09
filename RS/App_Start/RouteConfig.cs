using RS.Core;
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
            Database1Entities4 db = db = new Database1Entities4();
            QueryParameter param = QueryParameter.Create().Name("@u").Type(SqlDbType.NVarChar).Value("admin@admin.sk");
            var reader = SQLFactory.Instance.SelectFromWhere(
                @"[user_id]",
                @"[dbo].[Users]",
                @"[email] = @u",
                param);

            if (!reader.HasRows)
            {
                reader.Dispose();

                QueryParameter passwParameter = QueryParameter.Create().Name("@p").Type(SqlDbType.NVarChar).Value(Helpers.SHA1.Encode("admin1234"));
                QueryParameter emailParameter = QueryParameter.Create().Name("@e").Type(SqlDbType.NVarChar).Value("admin@admin.sk");
                QueryParameter firstNameParameter = QueryParameter.Create().Name("@a").Type(SqlDbType.NVarChar).Value("admin");
                QueryParameter lastNameParameter = QueryParameter.Create().Name("@b").Type(SqlDbType.NVarChar).Value("admin");
                QueryParameter phoneParameter = QueryParameter.Create().Name("@d").Type(SqlDbType.NVarChar).Value("0");

                SQLFactory.Instance.InsertInto(
                    @"Users",
                    @"email,first_name,last_name,password,phone_number",
                    @"@e,@a,@b,@p,@d",
                    passwParameter,
                    emailParameter,
                    firstNameParameter,
                    lastNameParameter,
                    phoneParameter);

                int user_id = Int32.Parse(SQLFactory.Instance.SelectFromWhereScalar(
                    @"*",
                    @"Users",
                    @"first_name = 'admin'",
                    null).ToString()
                    );

                QueryParameter userParameter = QueryParameter.Create().Name("@u").Type(SqlDbType.NVarChar).Value(user_id);
                QueryParameter roleParameter = QueryParameter.Create().Name("@r").Type(SqlDbType.NVarChar).Value(1);
                SQLFactory.Instance.InsertInto(@"UsersRoles", @"user_id,roles_id", @"@u,@r", userParameter, roleParameter);
            }
            else
            {
                reader.Dispose();
            }
        }
    }
}

