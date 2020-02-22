using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ProtocolProfileConfiguration : IEntityTypeConfiguration<ProtocolProfile>
    {
        public void Configure(EntityTypeBuilder<ProtocolProfile> entity)
        {
            entity.HasKey(e => e.i_ProtocolProfileId);

            entity.ToTable("ProtocolProfile", "commercial");

            entity.Property(e => e.i_ProtocolProfileId).HasColumnName("i_ProtocolProfileId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_Name)
                .IsRequired()
                .HasColumnName("v_Name")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
