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
    
    public partial class Trainings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trainings()
        {
            this.TrainingUsers = new HashSet<TrainingUsers>();
        }
    
        public int user_id { get; set; }
        public int training_id { get; set; }
        public System.DateTime time { get; set; }
        public int kapacita { get; set; }
    
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrainingUsers> TrainingUsers { get; set; }
    }
}