using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class ComponentConfiguration : IEntityTypeConfiguration<Component>
    {
        public void Configure(EntityTypeBuilder<Component> entity)
        {
            entity.HasKey(e => e.v_ComponentId);

            entity.HasIndex(e => e.v_ComponentId);
            entity.Property(e => e.v_Name).HasColumnName("v_Name");
            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");
            entity.Property(e => e.r_CostPrice).HasColumnName("r_CostPrice");
            entity.Property(e => e.r_BasePrice).HasColumnName("r_BasePrice");
            entity.Property(e => e.r_SalePrice).HasColumnName("r_SalePrice");
            entity.Property(e => e.i_DiagnosableId).HasColumnName("i_DiagnosableId");
            entity.Property(e => e.i_IsApprovedId).HasColumnName("i_IsApprovedId");
            entity.Property(e => e.i_ComponentTypeId).HasColumnName("i_ComponentTypeId");
            entity.Property(e => e.i_UIIsVisibleId).HasColumnName("i_UIIsVisibleId");
            entity.Property(e => e.i_UIIndex).HasColumnName("i_UIIndex");
            entity.Property(e => e.i_ValidInDays).HasColumnName("i_ValidInDays");
            entity.Property(e => e.i_GroupedComponentId).HasColumnName("i_GroupedComponentId");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
        }
    }
}
