using System.Data;
using System.Data.SqlClient;

namespace RS.Models
{
    public class UserValidator
    {
        public static bool IsValid(string email, string _password)
        {
            QueryParameter emailParameter = QueryParameter.Create()
                .Name("@u")
                .Type(SqlDbType.NVarChar)
                .Value(email);

            QueryParameter passwParameter = QueryParameter.Create()
                .Name("@p")
                .Type(SqlDbType.NVarChar)
                .Value(Helpers.SHA1.Encode(_password));

            var reader = SQLFactory.Instance.SelectFromWhere(
                @"[user_id]",
                @"[dbo].[Users]",
                @"[email] = @u AND [password] = @p",
                emailParameter,
                passwParameter);

            if (reader.HasRows)
            {
                reader.Dispose();
                return true;
            }
            else
            {
                reader.Dispose();
                return false;
            }
        }
    }
}
