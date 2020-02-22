using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
    {
        public void Configure(EntityTypeBuilder<Quotation> entity)
        {
            entity.HasKey(e => e.i_QuotationId)
                .HasName("PK_Quatation");

            entity.ToTable("Quotation", "commercial");

            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");

            entity.Property(e => e.d_AcceptanceDate)
                .HasColumnName("d_AcceptanceDate")
                .HasColumnType("datetime");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_ShippingDate)
                .HasColumnName("d_ShippingDate")
                .HasColumnType("datetime");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_CompanyHeadquarterId).HasColumnName("i_CompanyHeadquarterId");

            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_IsProccess).HasColumnName("i_IsProccess");

            entity.Property(e => e.i_StatusQuotationId).HasColumnName("i_StatusQuotationId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.i_UserCreatedId).HasColumnName("i_UserCreatedId");

            entity.Property(e => e.i_Version).HasColumnName("i_Version");

            entity.Property(e => e.r_TotalQuotation)
                .HasColumnName("r_TotalQuotation")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.v_Code)
                .IsRequired()
                .HasColumnName("v_Code")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.v_CommercialTerms)
                .IsRequired()
                .HasColumnName("v_CommercialTerms")
                .HasMaxLength(800)
                .IsUnicode(false);

            entity.Property(e => e.v_Email)
                .IsRequired()
                .HasColumnName("v_Email")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.v_FullName)
                .IsRequired()
                .HasColumnName("v_FullName")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.CompanyHeadquarter)
                .WithMany(p => p.Quotation)
                .HasForeignKey(d => d.i_CompanyHeadquarterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Quotation_CompanyHeadquarter");

            entity.HasOne(d => d.Company)
                .WithMany(p => p.Quotation)
                .HasForeignKey(d => d.i_CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Quotation_Company");

            entity.HasOne(d => d.UserCreated)
                .WithMany(p => p.Quotation)
                .HasForeignKey(d => d.i_UserCreatedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Quotation_SystemUser");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
