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
            entity.HasKey(e => e.i_AdditionalComponentsQuoteId)
              .HasName("PK_AdditionalComponentsQuote");

            entity.ToTable("AdditionalComponentsQuote", "commercial");
            entity.HasIndex(e => e.i_AdditionalComponentsQuoteId);
            entity.Property(e => e.i_AdditionalComponentsQuoteId).HasColumnName("i_AdditionalComponentsQuoteId");
            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");
            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");
            entity.Property(e => e.v_CategoryName).HasColumnName("v_CategoryName");
            entity.Property(e => e.v_ComponentId).HasColumnName("v_ComponentId");
            entity.Property(e => e.v_ComponentName).HasColumnName("v_ComponentName");
            entity.Property(e => e.r_MinPrice).HasColumnName("r_MinPrice");
            entity.Property(e => e.r_PriceList).HasColumnName("r_PriceList");
            entity.Property(e => e.r_SalePrice).HasColumnName("r_SalePrice");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
        }
    }
}
