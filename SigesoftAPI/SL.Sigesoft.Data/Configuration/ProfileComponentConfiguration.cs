using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ProfileComponentConfiguration : IEntityTypeConfiguration<ProfileComponent>
    {
        public void Configure(EntityTypeBuilder<ProfileComponent> entity)
        {
            entity.HasKey(e => e.i_ProfileComponentId)
              .HasName("PK_ProfileComponent");

            entity.ToTable("ProfileComponent", "commercial");
            entity.HasIndex(e => e.i_ProfileComponentId);
            entity.Property(e => e.i_ProfileComponentId).HasColumnName("i_ProfileComponentId");            
            entity.Property(e => e.i_QuotationProfileId).HasColumnName("i_QuotationProfileId");
            entity.Property(e => e.i_CategoryId).HasColumnName("i_CategoryId");
            entity.Property(e => e.v_CategoryName).HasColumnName("v_CategoryName");            
            entity.Property(e => e.v_ComponentId).HasColumnName("v_ComponentId");
            entity.Property(e => e.v_ComponentName).HasColumnName("v_ComponentName");            
            entity.Property(e => e.r_MinPrice).HasColumnName("r_MinPrice");
            entity.Property(e => e.r_PriceList).HasColumnName("r_PriceList");
            entity.Property(e => e.r_SalePrice).HasColumnName("r_SalePrice");

            entity.Property(e => e.i_AgeConditionalId).HasColumnName("i_AgeConditionalId");
            entity.Property(e => e.i_Age).HasColumnName("i_Age");
            entity.Property(e => e.i_GenderConditionalId).HasColumnName("i_GenderConditionalId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
        }
    }
}
