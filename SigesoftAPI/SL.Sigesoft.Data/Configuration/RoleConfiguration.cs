using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.HasKey(e => e.i_RoleId);

            entity.ToTable("Role", "security");

            entity.Property(e => e.i_RoleId).HasColumnName("i_RoleId");

            entity.Property(e => e.v_Description)
                .HasColumnName("v_Description")
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
