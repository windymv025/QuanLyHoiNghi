//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyHoiNghi.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIADIEMTOCHUC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIADIEMTOCHUC()
        {
            this.HOINGHIs = new HashSet<HOINGHI>();
        }
    
        public int IDDD { get; set; }
        public string TENDD { get; set; }
        public string DIACHI { get; set; }
        public int SUCCHUA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOINGHI> HOINGHIs { get; set; }
    }
}
