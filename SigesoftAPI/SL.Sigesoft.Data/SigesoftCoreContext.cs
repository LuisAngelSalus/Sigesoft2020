﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SL.Sigesoft.Data.Configuration;
using SL.Sigesoft.Models;

namespace SL.Sigesoft.Data
{
    public partial class SigesoftCoreContext : DbContext
    {
        public SigesoftCoreContext()
        {
        }

        public SigesoftCoreContext(DbContextOptions<SigesoftCoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Access> Access { get; set; }
        public virtual DbSet<ApplicationHierarchy> ApplicationHierarchy { get; set; }
        public virtual DbSet<OwnerCompany> OwnerCompany { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SystemParameter> SystemParameter { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<Company> Company{ get; set; }
        public virtual DbSet<CompanyHeadquarter> CompanyHeadquarters { get; set; }
        public virtual DbSet<CompanyContact> CompanyContact { get; set; }
        public virtual DbSet<Info> Info { get; set; }
        public virtual DbSet<Ubigeo> Ubigeo { get; set; }
        public virtual DbSet<Detail> Detail { get; set; }
        public virtual DbSet<ProtocolProfile> ProtocolProfile { get; set; }
        public virtual DbSet<ProfileDetail> ProfileDetail { get; set; }
        public virtual DbSet<Quotation> Quotation { get; set; }
        public virtual DbSet<QuotationProfile> QuotationProfile { get; set; }
        public virtual DbSet<ProfileComponent> ProfileComponent { get; set; }
        public virtual DbSet<AdditionalComponentsQuote> AdditionalComponentsQuote { get; set; }
        public virtual DbSet<Secuential> Secuential { get; set; }
        public virtual DbSet<QuoteTracking> QuoteTracking { get; set; }
        public virtual DbSet<PriceList> PriceList { get; set; }
        public virtual DbSet<Protocol> Protocol { get; set; }
        public virtual DbSet<ProtocolDetail> ProtocolDetail { get; set; }
        public virtual DbSet<Suscription> Suscription { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<AccountSetting> AccountSetting { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
        public virtual DbSet<ClientUser> ClientUser { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceComponent> ServiceComponent { get; set; }
        public virtual DbSet<Component> Component { get; set; }

        public virtual DbSet<Warehouse> Warehouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-servicing-10079");

            modelBuilder.ApplyConfiguration(new AccessConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationHierarchyConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerCompanyConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SystemParameterConfiguration());
            modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyHeadquarterConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyContactConfiguration());
            modelBuilder.ApplyConfiguration(new InfoConfiguration());
            modelBuilder.ApplyConfiguration(new UbigeoConfiguration());
            modelBuilder.ApplyConfiguration(new DetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolProfileConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileDetailConfiguration());
            modelBuilder.ApplyConfiguration(new QuotationConfiguration());
            modelBuilder.ApplyConfiguration(new QuotationProfileConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileComponentConfiguration());
            modelBuilder.ApplyConfiguration(new SecuentialConfiguration());
            modelBuilder.ApplyConfiguration(new QuoteTrackingConfiguration());
            modelBuilder.ApplyConfiguration(new AdditionalComponentsQuoteConfiguration());
            modelBuilder.ApplyConfiguration(new PriceListConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolConfiguration());
            modelBuilder.ApplyConfiguration(new ProtocolDetailConfiguration());
            modelBuilder.ApplyConfiguration(new SuscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new AccountSettingConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new ClientUserConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceComponentConfiguration());
            modelBuilder.ApplyConfiguration(new ComponentConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
        }
    }
}
