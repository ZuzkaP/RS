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
    
    public partial class users_roles
    {
        public int users_roles_id { get; set; }
        public Nullable<int> users_id { get; set; }
        public Nullable<int> roles_id { get; set; }
    
        public virtual Roles Roles { get; set; }
        public virtual Users Users { get; set; }
    }
}
