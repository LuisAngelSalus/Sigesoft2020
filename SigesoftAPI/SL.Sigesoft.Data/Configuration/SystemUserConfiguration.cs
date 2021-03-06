﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> entity)
        {
            entity.HasKey(e => e.i_SystemUserId)
                .HasName("PK_systemuser");

            entity.ToTable("SystemUser", "security");

            entity.Property(e => e.i_SystemUserId).HasColumnName("i_SystemUserId");

            entity.Property(e => e.i_CustomerCompanyId).HasColumnName("i_CustomerCompanyId");          

            entity.Property(e => e.i_PersonId).HasColumnName("i_PersonId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.v_Email)
                .IsRequired()
                .HasColumnName("v_Email")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.v_Password)
                .IsRequired()
                .HasColumnName("v_Password")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.v_Phone)
                .HasColumnName("v_Phone")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.v_UserName)
                .IsRequired()
                .HasColumnName("v_UserName")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Person)
                .WithMany(p => p.SystemUser)
                .HasForeignKey(d => d.i_PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_systemuser_person");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
