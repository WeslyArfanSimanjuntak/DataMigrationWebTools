﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MIGULDBEntitiesNew : DbContext
    {
        public MIGULDBEntitiesNew()
            : base("name=MIGULDBEntitiesNew")
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
        public virtual DbSet<sw_address_type> sw_address_type { get; set; }
        public virtual DbSet<sw_education> sw_education { get; set; }
        public virtual DbSet<sw_identity_type> sw_identity_type { get; set; }
        public virtual DbSet<uw_underwriting> uw_underwriting { get; set; }
        public virtual DbSet<uw_underwriting_detail> uw_underwriting_detail { get; set; }
        public virtual DbSet<sw_alteration_type> sw_alteration_type { get; set; }
        public virtual DbSet<sw_application_status> sw_application_status { get; set; }
        public virtual DbSet<sw_approval_status> sw_approval_status { get; set; }
        public virtual DbSet<sw_area> sw_area { get; set; }
        public virtual DbSet<sw_benefit_type> sw_benefit_type { get; set; }
        public virtual DbSet<sw_city> sw_city { get; set; }
        public virtual DbSet<sw_claim_document> sw_claim_document { get; set; }
        public virtual DbSet<sw_claim_reason> sw_claim_reason { get; set; }
        public virtual DbSet<sw_claim_status> sw_claim_status { get; set; }
        public virtual DbSet<sw_client_type> sw_client_type { get; set; }
        public virtual DbSet<sw_cod> sw_cod { get; set; }
        public virtual DbSet<sw_disease> sw_disease { get; set; }
        public virtual DbSet<sw_disease_type> sw_disease_type { get; set; }
        public virtual DbSet<sw_distribution_channel> sw_distribution_channel { get; set; }
        public virtual DbSet<sw_investment_type> sw_investment_type { get; set; }
        public virtual DbSet<sw_line_business> sw_line_business { get; set; }
        public virtual DbSet<sw_marital_status> sw_marital_status { get; set; }
        public virtual DbSet<sw_occupation> sw_occupation { get; set; }
        public virtual DbSet<sw_occupation_category> sw_occupation_category { get; set; }
        public virtual DbSet<sw_office> sw_office { get; set; }
        public virtual DbSet<sw_office_type> sw_office_type { get; set; }
        public virtual DbSet<sw_payment_frequency> sw_payment_frequency { get; set; }
        public virtual DbSet<sw_payment_method> sw_payment_method { get; set; }
        public virtual DbSet<sw_payment_mode> sw_payment_mode { get; set; }
        public virtual DbSet<sw_payment_status> sw_payment_status { get; set; }
        public virtual DbSet<sw_payment_type> sw_payment_type { get; set; }
        public virtual DbSet<sw_policy_status> sw_policy_status { get; set; }
        public virtual DbSet<sw_position> sw_position { get; set; }
        public virtual DbSet<sw_product_type> sw_product_type { get; set; }
        public virtual DbSet<sw_provider> sw_provider { get; set; }
        public virtual DbSet<sw_province> sw_province { get; set; }
        public virtual DbSet<sw_reasurance_type> sw_reasurance_type { get; set; }
        public virtual DbSet<sw_regency> sw_regency { get; set; }
        public virtual DbSet<sw_regional> sw_regional { get; set; }
        public virtual DbSet<sw_religion> sw_religion { get; set; }
        public virtual DbSet<sw_risk_class> sw_risk_class { get; set; }
        public virtual DbSet<sw_transaction_type> sw_transaction_type { get; set; }
        public virtual DbSet<fn_bank_bi> fn_bank_bi { get; set; }
    }
}