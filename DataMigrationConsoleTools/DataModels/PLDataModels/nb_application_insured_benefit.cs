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
    
    public partial class nb_application_insured_benefit
    {
        public string application_no { get; set; }
        public string insured_no { get; set; }
        public string product_code { get; set; }
        public string plan_code { get; set; }
        public string benefit_code { get; set; }
        public string benefit_group_code { get; set; }
        public decimal benefit_length_days { get; set; }
        public decimal benefit_percentage { get; set; }
        public decimal benefit_amount { get; set; }
        public string benefit_status { get; set; }
        public Nullable<System.DateTime> effective_date { get; set; }
        public Nullable<System.DateTime> expire_date { get; set; }
        public Nullable<decimal> masa_pencairan_bunga { get; set; }
    }
}
