using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ProfileDetailConfiguration : IEntityTypeConfiguration<ProfileDetail>
    {
        public void Configure(EntityTypeBuilder<ProfileDetail> entity)
        {
            entity.HasKey(e => e.i_ProfileDetailId);

            entity.ToTable("ProfileDetail", "commercial");

            entity.Property(e => e.i_ProfileDetailId).HasColumnName("i_ProfileDetailId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_ProtocolProfileId).HasColumnName("i_ProtocolProfileId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.r_ListPrice)
                .HasColumnName("r_ListPrice")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.r_MinPrice)
                .HasColumnName("r_MinPrice")
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

            entity.HasOne(d => d.ProtocolProfile)
                .WithMany(p => p.ProfileDetail)
                .HasForeignKey(d => d.i_ProtocolProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProfileDetail_ProtocolProfile");
        }
    }
}
