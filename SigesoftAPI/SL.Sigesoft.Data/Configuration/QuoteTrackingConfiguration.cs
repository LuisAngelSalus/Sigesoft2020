using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class QuoteTrackingConfiguration : IEntityTypeConfiguration<QuoteTracking>
    {
        public void Configure(EntityTypeBuilder<QuoteTracking> entity)
        {
            entity.HasKey(e => e.i_QuoteTrackingId);

            entity.ToTable("QuoteTracking", "commercial");

            entity.Property(e => e.i_QuoteTrackingId).HasColumnName("i_QuoteTrackingId");

            entity.Property(e => e.d_Date)
                .HasColumnName("d_Date")
                .HasColumnType("datetime");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_Commentary)
                .IsRequired()
                .HasColumnName("v_Commentary")
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.v_StatusName)
                .IsRequired()
                .HasColumnName("v_StatusName")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Quotation)
                .WithMany(p => p.QuoteTracking)
                .HasForeignKey(d => d.i_QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuoteTracking_Quotation");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
