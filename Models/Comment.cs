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
    
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            this.SubComments = new HashSet<SubComment>();
        }
    
        public int ComID { get; set; }
        public string CommentMsg { get; set; }
        public System.DateTime CommentedDate { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual SinhVien SinhVien { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubComment> SubComments { get; set; }
    }
}