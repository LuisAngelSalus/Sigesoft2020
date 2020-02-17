using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using SL.Sigesoft.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {                 
            //pk
            entity.HasKey(e => e.i_CompanyId).HasName("PK_company");
            //tables - schemes
            entity.ToTable("Company", "commercial");
            //indexes
            entity.HasIndex(e => e.i_CompanyId);
            //properties
            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");
            entity.Property(e => e.v_Name).HasColumnName("v_Name").IsRequired();
            entity.Property(e => e.v_IdentificationNumber).HasColumnName("v_IdentificationNumber");
            entity.Property(e => e.v_Address).HasColumnName("v_Address");
            entity.Property(e => e.v_PhoneNumber).HasColumnName("v_PhoneNumber");
            entity.Property(e => e.v_ContactName).HasColumnName("v_ContactName");
            entity.Property(e => e.v_Mail).HasColumnName("v_Mail");
            entity.Property(e => e.v_District).HasColumnName("v_District");
            entity.Property(e => e.v_PhoneCompany).HasColumnName("v_PhoneCompany");
              
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            //validations
            entity.Property(e => e.v_Name)
               .HasColumnName("v_Name")
               .HasMaxLength(250)
               .IsUnicode(false);

            entity.Property(e => e.v_IdentificationNumber)
               .HasColumnName("v_IdentificationNumber")
               .HasMaxLength(20)
               .IsUnicode(false);

            entity.Property(e => e.v_Address)
               .HasColumnName("v_Address")
               .HasMaxLength(250)
               .IsUnicode(false);

            entity.Property(e => e.v_PhoneNumber)
               .HasColumnName("v_PhoneNumber")
               .HasMaxLength(100)
               .IsUnicode(false);

            entity.Property(e => e.v_ContactName)
               .HasColumnName("v_ContactName")
               .HasMaxLength(150)
               .IsUnicode(false);

            entity.Property(e => e.v_Mail)
               .HasColumnName("v_Mail")
               .HasMaxLength(150)
               .IsUnicode(false);

            entity.Property(e => e.v_District)
           .HasColumnName("v_District")
           .HasMaxLength(100)
           .IsUnicode(false);

            entity.Property(e => e.v_PhoneCompany)
           .HasColumnName("v_PhoneCompany")
           .HasMaxLength(50)
           .IsUnicode(false);

            entity.HasOne(d => d.CompanyHeadquarter)
               .WithMany(p => p.Company)
               .HasForeignKey(d => d.i_CompanyId)
               .HasConstraintName("FK_Company_CompanyHeadquarter");

            //filters
            entity.HasQueryFilter(x => x.i_IsDeleted == YesNo.No);
        }
    }
}
