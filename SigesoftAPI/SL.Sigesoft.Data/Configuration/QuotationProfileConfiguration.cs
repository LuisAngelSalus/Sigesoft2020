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
                .HasName("PK_QuatationDetail");

            entity.ToTable("QuotationProfile", "commercial");

            entity.Property(e => e.i_QuotationProfileId).HasColumnName("i_QuotationProfileId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");

            entity.Property(e => e.i_ServiceTypeId).HasColumnName("i_ServiceTypeId");

            entity.Property(e => e.i_TypeFormatId).HasColumnName("i_TypeFormatId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_ProfileName)
                .IsRequired()
                .HasColumnName("v_ProfileName")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Quotation)
                .WithMany(p => p.QuotationProfile)
                .HasForeignKey(d => d.i_QuotationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuotationProfile_Quotation");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
