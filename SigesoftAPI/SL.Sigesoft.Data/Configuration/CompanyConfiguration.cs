using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.HasKey(e => e.i_CompanyId)
                    .HasName("PK_company");

            entity.ToTable("Company", "commercial");

            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");
            entity.Property(e => e.v_Name).HasColumnName("v_Name");
            entity.Property(e => e.v_IdentificationNumber).HasColumnName("v_IdentificationNumber");
            entity.Property(e => e.v_Address).HasColumnName("v_Address");
            entity.Property(e => e.v_PhoneNumber).HasColumnName("v_PhoneNumber");
            entity.Property(e => e.v_ContacName).HasColumnName("v_ContacName");
            entity.Property(e => e.v_Mail).HasColumnName("v_Mail");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.v_Name)
               .HasColumnName("v_Name")
               .HasMaxLength(250)
               .IsUnicode(false);

            entity.Property(e => e.v_IdentificationNumber)
               .HasColumnName("v_IdentificationNumber")
               .HasMaxLength(250)
               .IsUnicode(false);

            entity.Property(e => e.v_Address)
               .HasColumnName("v_Address")
               .HasMaxLength(250)
               .IsUnicode(false);

            entity.Property(e => e.v_PhoneNumber)
               .HasColumnName("v_PhoneNumber")
               .HasMaxLength(100)
               .IsUnicode(false);

            entity.Property(e => e.v_ContacName)
               .HasColumnName("v_ContacName")
               .HasMaxLength(150)
               .IsUnicode(false);

            entity.Property(e => e.v_Mail)
               .HasColumnName("v_Mail")
               .HasMaxLength(150)
               .IsUnicode(false);

            entity.HasOne(d => d.CompanyHeadquarter)
               .WithMany(p => p.Company)
               .HasForeignKey(d => d.i_CompanyId)
               .HasConstraintName("FK_Company_CompanyHeadquarter");
        }
    }
}
