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
    
    public partial class Cate_News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cate_News()
        {
            this.News = new HashSet<News>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Id_Khoa { get; set; }
    
        public virtual Khoa Khoa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News> News { get; set; }
    }
}
