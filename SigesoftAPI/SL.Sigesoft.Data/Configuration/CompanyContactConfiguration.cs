using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class CompanyContactConfiguration : IEntityTypeConfiguration<CompanyContact>
    {
        public void Configure(EntityTypeBuilder<CompanyContact> entity)
        {
            entity.HasKey(e => e.i_CompanyContactId)
                      .HasName("PK_CompanyContact_1");

            entity.ToTable("CompanyContact", "commercial");

            entity.Property(e => e.i_CompanyContactId).HasColumnName("i_CompanyContactId");

            entity.Property(e => e.i_CompanyHeadquarterId).HasColumnName("i_CompanyHeadquarterId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.v_CM)
                .HasColumnName("v_CM")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.v_Dni)
                .HasColumnName("v_Dni")
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.v_Email)
                .HasColumnName("v_Email")
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.v_FullName)
                .IsRequired()
                .HasColumnName("v_FullName")
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.v_Phone)
                .HasColumnName("v_Phone")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.v_TypeUs)
                .HasColumnName("v_TypeUs")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
