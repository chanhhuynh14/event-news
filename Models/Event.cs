//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_Hutech.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.SV_SK = new HashSet<SV_SK>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public string Content { get; set; }
        public string Image1 { get; set; }
        public string icon { get; set; }
        public string Keyword { get; set; }
        public string SeoTitle { get; set; }
        public Nullable<int> Id_Cate { get; set; }
        public Nullable<double> SL_Thamgia { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string DKSK { get; set; }
        public string DiaDiem { get; set; }
    
        public virtual Category_Event Category_Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SV_SK> SV_SK { get; set; }
    }
}
