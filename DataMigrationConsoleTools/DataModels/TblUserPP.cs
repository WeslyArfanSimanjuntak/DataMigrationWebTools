//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataMigrationConsoleTools.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblUserPP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblUserPP()
        {
            this.TblUserTokenPP = new HashSet<TblUserTokenPP>();
        }
    
        public string User_Id { get; set; }
        public string User_Password { get; set; }
        public byte[] User_Image { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public Nullable<System.DateTime> Modify_Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblUserTokenPP> TblUserTokenPP { get; set; }
    }
}
