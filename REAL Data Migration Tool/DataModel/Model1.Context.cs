﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace REAL_Data_Migration_Tool.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MultiUserDBEntities : DbContext
    {
        public MultiUserDBEntities()
            : base("name=MultiUserDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AppVersion> AppVersion { get; set; }
        public virtual DbSet<BreakPolisInvesta> BreakPolisInvesta { get; set; }
        public virtual DbSet<CancelPolisIIL> CancelPolisIIL { get; set; }
        public virtual DbSet<CancelPolisInvesta> CancelPolisInvesta { get; set; }
        public virtual DbSet<CommonSetting> CommonSetting { get; set; }
        public virtual DbSet<DataNAV> DataNAV { get; set; }
        public virtual DbSet<DataNAVNew> DataNAVNew { get; set; }
        public virtual DbSet<DataNAVold> DataNAVold { get; set; }
        public virtual DbSet<EndorsPolisIIL> EndorsPolisIIL { get; set; }
        public virtual DbSet<EndorsPolisInvesta> EndorsPolisInvesta { get; set; }
        public virtual DbSet<IIL_CashBack> IIL_CashBack { get; set; }
        public virtual DbSet<IIL_CashReward> IIL_CashReward { get; set; }
        public virtual DbSet<IIL_EOM> IIL_EOM { get; set; }
        public virtual DbSet<IILSchedule> IILSchedule { get; set; }
        public virtual DbSet<IILTransaction> IILTransaction { get; set; }
        public virtual DbSet<Investa_CashBack> Investa_CashBack { get; set; }
        public virtual DbSet<Investa_CashReward> Investa_CashReward { get; set; }
        public virtual DbSet<InvestaEOM> InvestaEOM { get; set; }
        public virtual DbSet<InvestaSchedule> InvestaSchedule { get; set; }
        public virtual DbSet<InvestaTransaction> InvestaTransaction { get; set; }
        public virtual DbSet<InvestaTransactionNew> InvestaTransactionNew { get; set; }
        public virtual DbSet<InvestaTransactionold> InvestaTransactionold { get; set; }
        public virtual DbSet<IPP_CashReward> IPP_CashReward { get; set; }
        public virtual DbSet<IPPCancel> IPPCancel { get; set; }
        public virtual DbSet<IPPMature> IPPMature { get; set; }
        public virtual DbSet<IPPSchedule> IPPSchedule { get; set; }
        public virtual DbSet<PaymentReq_Detail> PaymentReq_Detail { get; set; }
        public virtual DbSet<PaymentReq_Header> PaymentReq_Header { get; set; }
        public virtual DbSet<PolisIIL> PolisIIL { get; set; }
        public virtual DbSet<PolisInvesta> PolisInvesta { get; set; }
        public virtual DbSet<PolisIPP> PolisIPP { get; set; }
        public virtual DbSet<PolisIPP_tmp> PolisIPP_tmp { get; set; }
        public virtual DbSet<PolisIPPARO> PolisIPPARO { get; set; }
        public virtual DbSet<PolisResponse> PolisResponse { get; set; }
        public virtual DbSet<PostpPolisInvesta> PostpPolisInvesta { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductRate> ProductRate { get; set; }
        public virtual DbSet<ProductRate_Detail> ProductRate_Detail { get; set; }
        public virtual DbSet<Rate_COI> Rate_COI { get; set; }
        public virtual DbSet<Reference> Reference { get; set; }
        public virtual DbSet<TblLabel> TblLabel { get; set; }
        public virtual DbSet<TblLabelNew> TblLabelNew { get; set; }
        public virtual DbSet<TblRole> TblRole { get; set; }
        public virtual DbSet<TblSOA> TblSOA { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserPP> TblUserPP { get; set; }
        public virtual DbSet<TblUserPPResponse> TblUserPPResponse { get; set; }
        public virtual DbSet<TblUserTokenPP> TblUserTokenPP { get; set; }
        public virtual DbSet<UpdateNAV> UpdateNAV { get; set; }
        public virtual DbSet<BUPRD> BUPRD { get; set; }
        public virtual DbSet<BUPRH> BUPRH { get; set; }
        public virtual DbSet<TblKodePos> TblKodePos { get; set; }
    }
}
