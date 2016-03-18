namespace RS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.SqlClient;

    public partial class Users
    {
        
        public int user_id { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
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

        public Nullable<int> roles_id { get; set; }

        public virtual Role Role { get; set; }

    }

}
    