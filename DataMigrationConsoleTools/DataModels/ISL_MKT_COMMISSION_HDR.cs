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
    
    public partial class ISL_MKT_COMMISSION_HDR
    {
        public int N_COMMISSION_HDR_ID { get; set; }
        public string C_COMMISSION_BATCH_NAME { get; set; }
        public System.DateTime D_START_DATE { get; set; }
        public System.DateTime D_END_DATE { get; set; }
        public Nullable<System.DateTime> D_PAID_DATE { get; set; }
        public string C_PAID_NOTES { get; set; }
        public string C_PAID_SLIP_IMG_PATH { get; set; }
        public string C_MEMO_NO { get; set; }
        public string C_MEMO_FILE_PATH { get; set; }
        public string C_STATUS { get; set; }
        public int N_CREATED_BY { get; set; }
        public System.DateTime D_CREATION_DATE { get; set; }
        public int N_LAST_UPDATED_BY { get; set; }
        public System.DateTime D_LAST_UPDATED_DATE { get; set; }
    }
}
