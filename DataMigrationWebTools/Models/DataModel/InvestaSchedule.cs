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
    
    public partial class InvestaSchedule
    {
        public string No_SPAJ { get; set; }
        public System.DateTime Eff_Date { get; set; }
        public Nullable<System.DateTime> Due_Date { get; set; }
        public Nullable<int> Day_Diff { get; set; }
        public Nullable<int> MPI { get; set; }
        public Nullable<int> MBI { get; set; }
        public Nullable<decimal> UP { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> InterestAmount { get; set; }
        public string Interest_Sts { get; set; }
        public string Create_UserId { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Modify_UserId { get; set; }
        public Nullable<System.DateTime> Modify_Date { get; set; }
    }
}
