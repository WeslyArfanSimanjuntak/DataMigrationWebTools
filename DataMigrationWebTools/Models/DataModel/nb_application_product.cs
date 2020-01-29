//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataMigrationWebTools.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class nb_application_product
    {
        public string application_no { get; set; }
        public string product_code { get; set; }
        public string plan_code { get; set; }
        public decimal insurance_period_year { get; set; }
        public Nullable<decimal> insurance_period_month { get; set; }
        public Nullable<decimal> unit_amount { get; set; }
        public decimal sum_assured { get; set; }
        public Nullable<decimal> insurance_cost_annual { get; set; }
        public decimal insurance_cost { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public string reinsurance_status { get; set; }
        public string treaty_code { get; set; }
        public decimal premium_rate { get; set; }
        public string main_status { get; set; }
        public string insured_no { get; set; }
        public string active_status { get; set; }
        public Nullable<decimal> extra_coi_annual_amt { get; set; }
        public Nullable<decimal> extra_coi_amt { get; set; }
        public Nullable<decimal> loading_amount { get; set; }
        public string fac_status { get; set; }
        public string fac_process_status { get; set; }
        public string fac_process_userid { get; set; }
        public Nullable<System.DateTime> fac_process_date { get; set; }
        public string claim_status { get; set; }
        public string reff_claim_id { get; set; }
        public string waive_status { get; set; }
        public Nullable<System.DateTime> waive_effective_date { get; set; }
    }
}
