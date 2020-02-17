using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class SystemParameterConfiguration : IEntityTypeConfiguration<SystemParameter>
    {
        public void Configure(EntityTypeBuilder<SystemParameter> entity)
        {
            entity.HasKey(e => new { e.i_GroupId, e.i_ParameterId })
                .HasName("PK_systemparameter");

            entity.ToTable("SystemParameter", "common");

            entity.Property(e => e.i_GroupId).HasColumnName("i_GroupId");

            entity.Property(e => e.i_ParameterId).HasColumnName("i_ParameterId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_Value1)
                .HasColumnName("v_Value1")
                .HasMaxLength(8000)
                .IsUnicode(false);

            entity.Property(e => e.v_Value2)
                .HasColumnName("v_Value2")
                .HasMaxLength(8000)
                .IsUnicode(false);
        }
    }
}
