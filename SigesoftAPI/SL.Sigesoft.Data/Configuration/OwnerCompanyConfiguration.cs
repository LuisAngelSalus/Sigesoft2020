﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class OwnerCompanyConfiguration : IEntityTypeConfiguration<OwnerCompany>
    {
        public void Configure(EntityTypeBuilder<OwnerCompany> entity)
        {
            entity.HasKey(e => e.i_OwnerCompanyId);

            entity.ToTable("OwnerCompany", "security");

            entity.Property(e => e.i_OwnerCompanyId).HasColumnName("i_OwnerCompanyId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_BusinessName)
                .HasColumnName("v_BusinessName")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.v_IdentificationNumber)
                .HasColumnName("v_IdentificationNumber")
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
