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
    
    public partial class ADDRESS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADDRESS()
        {
            this.APPLICATION = new HashSet<APPLICATION>();
        }
    
        public long ADDR_ID { get; set; }
        public string ADDR_TYPE { get; set; }
        public string ADDR1 { get; set; }
        public string ADDR2 { get; set; }
        public string ADDR3 { get; set; }
        public string POSTCODE { get; set; }
        public string CITY { get; set; }
        public string PROVINCE { get; set; }
        public string COUNTRY { get; set; }
        public string TELP { get; set; }
        public string HPNO { get; set; }
        public string FAXNO { get; set; }
        public string EMAIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLICATION> APPLICATION { get; set; }
    }
}
