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
    
    public partial class Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Roles()
        {
            this.UsersRoles = new HashSet<UsersRoles>();
        }
    
        public int roles_id { get; set; }
        public string name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersRoles> UsersRoles { get; set; }

        public override bool Equals(object obj)
        {
            if(!(obj is Roles))
            {
                return false;
            }

            if(this == obj)
            {
                return true;
            }
            Roles other = (Roles)obj;
            return name.Equals(other.name);
        }
    }
}
