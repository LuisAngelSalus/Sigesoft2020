using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.HasKey(e => e.i_PermissionId);

            entity.ToTable("Permission", "security");

            entity.Property(e => e.i_PermissionId).HasColumnName("i_PermissionId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_RoleId).HasColumnName("i_RoleId");

            entity.Property(e => e.i_SystemUserId).HasColumnName("i_SystemUserId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Permission)
                .HasForeignKey(d => d.i_RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security.Permission_Security.Role");

            entity.HasOne(d => d.SystemUser)
                .WithMany(p => p.Permission)
                .HasForeignKey(d => d.i_SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Security.Permission_Security.SystemUser");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
