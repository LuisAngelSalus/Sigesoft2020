using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class WarehouseConfiguration: IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> entity)
        {
            entity.HasKey(e => e.i_WarehouseId)
               .HasName("PK_Warehouse");

            entity.ToTable("Warehouse", "logistics");


            entity.Property(e => e.i_WarehouseId).HasColumnName("i_WarehouseId");
            
            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");
            entity.Property(e => e.i_CompanyHeadquarterId).HasColumnName("i_CompanyHeadquarterId");
            entity.Property(e => e.v_Description).HasColumnName("v_Description");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_IsPrincipal).HasColumnName("i_IsPrincipal");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.HasOne(d => d.Company)
             .WithMany(p => p.Warehouse)
             .HasForeignKey(d => d.i_CompanyId)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_Warehouse_Company");

            entity.HasOne(d => d.CompanyHeadquarter)
               .WithMany(p => p.Warehouse)
               .HasForeignKey(d => d.i_CompanyHeadquarterId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Warehouse_CompanyHeadquarter");

            entity.Property(e => e.v_Description)
                .IsRequired()
                .HasColumnName("v_Description")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}

