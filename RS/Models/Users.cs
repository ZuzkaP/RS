//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;

    public partial class Users
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.users_roles = new HashSet<users_roles>();
        }

        public int user_id { get; set; }


        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Please provide valid email id")]
        public string email { get; set; }

        [Required]
        [Display(Name = "First name: ")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last name: ")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Phone number: ")]
        public string phone_number { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string password { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<users_roles> users_roles { get; set; }


        public bool IsValid(string email, string _password)
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


