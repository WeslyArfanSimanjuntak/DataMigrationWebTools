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
    
    public partial class PaymentReq_Detail
    {
        public string PR_Num { get; set; }
        public int PR_Id { get; set; }
        public Nullable<System.DateTime> PR_Date { get; set; }
        public string SPAJ { get; set; }
        public string Polis { get; set; }
        public Nullable<System.DateTime> Value_Date { get; set; }
        public string Trans_Type { get; set; }
        public Nullable<decimal> Trans_Amount { get; set; }
        public string BankId { get; set; }
        public string Bank_Branch { get; set; }
        public string Acc_Number { get; set; }
        public string Acc_Name { get; set; }
        public string PR_Sts { get; set; }
        public string Paid_ReffId { get; set; }
        public Nullable<decimal> Paid_Amount { get; set; }
        public Nullable<System.DateTime> Paid_Date { get; set; }
        public string Create_UserId { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Modify_UserId { get; set; }
        public Nullable<System.DateTime> Modify_Date { get; set; }
    }
}
