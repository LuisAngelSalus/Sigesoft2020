using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ProtocolDetailConfiguration : IEntityTypeConfiguration<ProtocolDetail>
    {
        public void Configure(EntityTypeBuilder<ProtocolDetail> entity)
        {
            entity.HasKey(e => e.i_ProtocolDetailId);

            entity.ToTable("ProtocolDetail", "protocol");

            entity.Property(e => e.i_ProtocolDetailId).HasColumnName("i_ProtocolDetailId");

            entity.Property(e => e.i_Age).HasColumnName("i_Age");

            entity.Property(e => e.i_AgeConditionalId).HasColumnName("i_AgeConditionalId");

            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");

            entity.Property(e => e.i_GenderConditionalId).HasColumnName("i_GenderConditionalId");
                        
            entity.Property(e => e.i_ProtocolId).HasColumnName("i_ProtocolId");

            entity.Property(e => e.i_QuotationProfileIdRef).HasColumnName("i_QuotationProfileIdRef");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

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
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.v_ComponentId)
                .IsRequired()
                .HasColumnName("v_ComponentId")
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.Property(e => e.v_ComponentName)
                .IsRequired()
                .HasColumnName("v_ComponentName")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Protocol)
                .WithMany(p => p.ProtocolDetail)
                .HasForeignKey(d => d.i_ProtocolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProtocolDetail_Protocol");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
