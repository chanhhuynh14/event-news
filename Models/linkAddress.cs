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
    
    public partial class linkAddress
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public linkAddress()
        {
            this.Events = new HashSet<Event>();
        }
    
        public int Id_Address { get; set; }
        public string Name_Address { get; set; }
        public string Full_Address { get; set; }
        public string Pb_Address { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
    }
}