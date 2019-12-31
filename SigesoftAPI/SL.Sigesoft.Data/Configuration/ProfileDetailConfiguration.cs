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
            entity.HasKey(e => e.i_ProfileDetailId)
                .HasName("PK_ProfileDetail");

            entity.ToTable("ProfileDetail", "protocol");
            entity.HasIndex(e => e.i_ProfileDetailId);
            entity.Property(e => e.i_ProfileDetailId).HasColumnName("i_ProfileDetailId");
            entity.Property(e => e.v_ComponentId).HasColumnName("v_ComponentId");
            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

        }
    }
}
