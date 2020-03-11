using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> entity)
        {
            entity.HasKey(e => e.i_ProfileId);

            entity.ToTable("Profile", "security");

            entity.Property(e => e.i_ProfileId).HasColumnName("i_ProfileId");

            entity.Property(e => e.i_ApplicationHierarchyId).HasColumnName("i_ApplicationHierarchyId");
            
            entity.Property(e => e.i_RoleId).HasColumnName("i_RoleId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.HasOne(d => d.ApplicationHierarchy)
                .WithMany(p => p.Profile)
                .HasForeignKey(d => d.i_ApplicationHierarchyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_applicationhierarchy");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Profile)
                .HasForeignKey(d => d.i_RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_Role");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
