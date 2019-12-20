using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ApplicationHierarchyConfiguration : IEntityTypeConfiguration<ApplicationHierarchy>
    {
        public void Configure(EntityTypeBuilder<ApplicationHierarchy> entity)
        {
            entity.HasKey(e => e.i_ApplicationHierarchyId)
                    .HasName("PK_applicationhierarchy");

            entity.ToTable("ApplicationHierarchy", "security");

            entity.Property(e => e.i_ApplicationHierarchyId).HasColumnName("i_ApplicationHierarchyId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_ParentId).HasColumnName("i_ParentId");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_Description)
                .HasColumnName("v_Description")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
