﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MIGULDBEntities : DbContext
    {
        public MIGULDBEntities()
            : base("name=MIGULDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cd_client> cd_client { get; set; }
        public virtual DbSet<fn_billing> fn_billing { get; set; }
        public virtual DbSet<fn_collection> fn_collection { get; set; }
        public virtual DbSet<fn_collection_detail> fn_collection_detail { get; set; }
        public virtual DbSet<fn_debitnote> fn_debitnote { get; set; }
        public virtual DbSet<fn_disbursement> fn_disbursement { get; set; }
        public virtual DbSet<fn_disbursement_detail> fn_disbursement_detail { get; set; }
        public virtual DbSet<fn_fundnav> fn_fundnav { get; set; }
        public virtual DbSet<nb_application> nb_application { get; set; }
        public virtual DbSet<nb_application_beneficiary> nb_application_beneficiary { get; set; }
        public virtual DbSet<nb_application_coi_history> nb_application_coi_history { get; set; }
        public virtual DbSet<nb_application_fund> nb_application_fund { get; set; }
        public virtual DbSet<nb_application_insured> nb_application_insured { get; set; }
        public virtual DbSet<nb_application_insured_benefit> nb_application_insured_benefit { get; set; }
        public virtual DbSet<nb_application_mgi> nb_application_mgi { get; set; }
        public virtual DbSet<nb_application_premium_history> nb_application_premium_history { get; set; }
        public virtual DbSet<nb_application_product> nb_application_product { get; set; }
        public virtual DbSet<ps_alteration> ps_alteration { get; set; }
        public virtual DbSet<ps_alteration_address> ps_alteration_address { get; set; }
        public virtual DbSet<ps_alteration_beneficiary> ps_alteration_beneficiary { get; set; }
        public virtual DbSet<ps_alteration_client> ps_alteration_client { get; set; }
        public virtual DbSet<ps_alteration_fund_allocation> ps_alteration_fund_allocation { get; set; }
        public virtual DbSet<ps_alteration_fund_allocation_detail> ps_alteration_fund_allocation_detail { get; set; }
        public virtual DbSet<ps_alteration_product> ps_alteration_product { get; set; }
        public virtual DbSet<ps_alteration_switching> ps_alteration_switching { get; set; }
        public virtual DbSet<ps_alteration_topup> ps_alteration_topup { get; set; }
        public virtual DbSet<ps_alteration_withdrawal> ps_alteration_withdrawal { get; set; }
        public virtual DbSet<ps_fund_transaction_history> ps_fund_transaction_history { get; set; }
        public virtual DbSet<uw_underwriting> uw_underwriting { get; set; }
        public virtual DbSet<uw_underwriting_detail> uw_underwriting_detail { get; set; }
    }
}
