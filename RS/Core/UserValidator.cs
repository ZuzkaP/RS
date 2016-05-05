using System.Data;
using System.Data.SqlClient;
using RS.Core;

namespace RS.Models
{
    public class UserValidator
    {
        public static bool IsValid(string email, string _password)
        {
            foreach(Users user in SQLConnection.Instance.Database.Users) 
            {
                if(user.email.Equals(email) && user.password.Equals(Helpers.SHA1.Encode(_password)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
