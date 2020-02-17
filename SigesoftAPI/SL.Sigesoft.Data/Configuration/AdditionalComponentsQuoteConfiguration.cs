using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class AdditionalComponentsQuoteConfiguration : IEntityTypeConfiguration<AdditionalComponentsQuote>
    {
        public void Configure(EntityTypeBuilder<AdditionalComponentsQuote> entity)
        {
            entity.HasKey(e => e.i_AdditionalComponentsQuoteId);

            entity.ToTable("AdditionalComponentsQuote", "commercial");

            entity.Property(e => e.i_AdditionalComponentsQuoteId).HasColumnName("i_AdditionalComponentsQuoteId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.r_MinPrice)
                .HasColumnName("r_MinPrice")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.r_PriceList)
                .HasColumnName("r_PriceList")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.r_SalePrice)
                .HasColumnName("r_SalePrice")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.v_CategoryName)
                .IsRequired()
                .HasColumnName("v_CategoryName")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.v_ComponentId)
                .IsRequired()
                .HasColumnName("v_ComponentId")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.v_ComponentName)
                .IsRequired()
                .HasColumnName("v_ComponentName")
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Quotation)
                .WithMany(p => p.AdditionalComponentsQuote)
                .HasForeignKey(d => d.i_QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdditionalComponentsQuote_Quotation");
        }
    }
}
