//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataMigrationConsoleTools.DataModels.PLDataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class ps_alteration_switching
    {
        public string alteration_id { get; set; }
        public System.DateTime trans_date { get; set; }
        public string switching_apply { get; set; }
        public Nullable<decimal> switching_fee { get; set; }
        public Nullable<System.DateTime> pricing_date { get; set; }
        public string posting_status { get; set; }
        public Nullable<System.DateTime> posting_date { get; set; }
        public string current_fundhouse_code { get; set; }
        public string alt_fundhouse_code { get; set; }
        public string reff_id { get; set; }
        public string voucherout_reff_id { get; set; }
        public string voucherin_reff_id { get; set; }
        public Nullable<decimal> neutralize_amount { get; set; }
    }
}