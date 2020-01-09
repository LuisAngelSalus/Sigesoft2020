using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data
{
    public class QuoteTrackingConfiguration : IEntityTypeConfiguration<QuoteTracking>
    {
        public void Configure(EntityTypeBuilder<QuoteTracking> entity)
        {
            entity.HasKey(e => e.i_QuoteTrackingId)
                    .HasName("PK_QuoteTracking");

            entity.ToTable("QuoteTracking", "commercial");
            entity.HasIndex(e => e.i_QuoteTrackingId);
            entity.Property(e => e.i_QuoteTrackingId).HasColumnName("i_QuoteTrackingId");

            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");
            entity.Property(e => e.d_Date).HasColumnName("d_Date");
            entity.Property(e => e.v_Commentary).HasColumnName("v_Commentary");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");


        }
    }
}
