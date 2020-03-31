using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> entity)
        {
            entity.HasKey(e => e.i_ComponentId);

            entity.ToTable("Component", "Medical");

            entity.Property(e => e.i_ComponentId).HasColumnName("i_ComponentId");

            entity.Property(e => e.v_ComponentId).HasColumnName("v_ComponentId");

            entity.Property(e => e.v_Name).HasColumnName("v_Name");

            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");

            entity.Property(e => e.r_CostPrice).HasColumnName("r_CostPrice");

            entity.Property(e => e.r_BasePrice).HasColumnName("r_BasePrice");

            entity.Property(e => e.r_SalePrice).HasColumnName("r_SalePrice");
        }
    }
}
