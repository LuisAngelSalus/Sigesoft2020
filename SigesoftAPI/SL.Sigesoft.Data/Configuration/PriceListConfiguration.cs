using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> entity)
        {
            entity.HasKey(e => e.i_PriceListId);

            entity.ToTable("PriceList", "commercial");

            entity.Property(e => e.i_PriceListId).HasColumnName("i_PriceListId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.r_Price)
                .HasColumnName("r_Price")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.v_ComponentId)
                .IsRequired()
                .HasColumnName("v_ComponentId")
                .HasMaxLength(16)
                .IsUnicode(false);

            entity.HasOne(d => d.Company)
                .WithMany(p => p.PriceList)
                .HasForeignKey(d => d.i_CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PriceList_Company");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
