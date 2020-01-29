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
    
    public partial class uw_underwriting_detail
    {
        public string medical_req_no { get; set; }
        public string product_code { get; set; }
        public System.DateTime decision_date { get; set; }
        public string decision_type_code { get; set; }
        public string subdecision_type_code { get; set; }
        public string substandard_type_code { get; set; }
        public Nullable<decimal> extra_premi_percent { get; set; }
        public Nullable<decimal> extra_mortality_permile { get; set; }
        public Nullable<int> postpone_duration { get; set; }
        public string decision_remark { get; set; }
        public string client_approval_status { get; set; }
        public string postpone_unit { get; set; }
        public Nullable<System.DateTime> client_approval_date { get; set; }
    }
}
