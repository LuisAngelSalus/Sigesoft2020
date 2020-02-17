using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class QuotationProfileConfiguration : IEntityTypeConfiguration<QuotationProfile>
    {
        public void Configure(EntityTypeBuilder<QuotationProfile> entity)
        {
            entity.HasKey(e => e.i_QuotationProfileId)
              .HasName("PK_quotationProfile");

            entity.ToTable("QuotationProfile", "commercial");
            entity.HasIndex(e => e.i_QuotationProfileId);
            entity.Property(e => e.i_QuotationProfileId).HasColumnName("i_QuotationProfileId");

            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");
            entity.Property(e => e.v_ProfileName).HasColumnName("v_ProfileName");
            entity.Property(e => e.i_ServiceTypeId).HasColumnName("i_ServiceTypeId");
            entity.Property(e => e.i_TypeFormatId).HasColumnName("i_TypeFormatId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
        }
    }
}
