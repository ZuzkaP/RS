using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RS.Models
{
    public class UserValidator
    {
        public static bool IsValid(string email, string _password)
        {
            using (var cn = new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\Zuzana\Documents\Visual Studio 2015\Projects\RS\RS\App_Data\Database1.mdf';Integrated Security=True"))
            {
                string _sql = @"SELECT [user_id] FROM [dbo].[Users] " +
                       @"WHERE [email] = @u AND [password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = email;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = Helpers.SHA1.Encode(_password);
                cn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
    }
}
   