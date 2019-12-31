using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class SystemParameterConfiguration : IEntityTypeConfiguration<SystemParameter>
    {
        public void Configure(EntityTypeBuilder<SystemParameter> entity)
        {            
            entity.HasKey(e => new { e.i_GroupId, e.i_ParameterId });

            entity.HasIndex(e =>new { e.i_GroupId, e.i_ParameterId });
            entity.Property(e => e.v_Value1).HasColumnName("v_Value1");
            entity.Property(e => e.v_Value2).HasColumnName("v_Value2");
            entity.Property(e => e.v_Field).HasColumnName("v_Field");
            entity.Property(e => e.i_ParentParameterId).HasColumnName("i_ParentParameterId");
            entity.Property(e => e.i_Sort).HasColumnName("i_Sort");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");            
        }
    }
}
    