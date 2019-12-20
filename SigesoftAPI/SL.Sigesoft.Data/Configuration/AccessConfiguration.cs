using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class AccessConfiguration : IEntityTypeConfiguration<Access>
    {
        public void Configure(EntityTypeBuilder<Access> entity)
        {
            entity.HasKey(e => e.i_AccessId);

            entity.ToTable("Access", "security");

            entity.Property(e => e.i_AccessId).HasColumnName("i_AccessId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_OwnerCompanyId).HasColumnName("i_OwnerCompanyId");

            entity.Property(e => e.i_PermissionId).HasColumnName("i_PermissionId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.HasOne(d => d.OwnerCompany)
                .WithMany(p => p.Access)
                .HasForeignKey(d => d.i_OwnerCompanyId)
                .HasConstraintName("FK_Access_OwnerCompany");

            entity.HasOne(d => d.Permission)
                .WithMany(p => p.Accesses)
                .HasForeignKey(d => d.i_PermissionId)
                .HasConstraintName("FK_Security.Access_Security.Permission");
        }
    }
}
