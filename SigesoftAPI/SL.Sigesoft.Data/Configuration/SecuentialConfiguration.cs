using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class SecuentialConfiguration : IEntityTypeConfiguration<Secuential>
    {
        public void Configure(EntityTypeBuilder<Secuential> entity)
        {
            entity.HasKey(e => e.i_SecuentialId)
              .HasName("PK_Secuential");

            entity.ToTable("Secuential", "common");
            entity.HasIndex(e => e.i_SecuentialId);
            entity.Property(e => e.i_SecuentialId).HasColumnName("i_SecuentialId");
            entity.Property(e => e.i_OwnerCompanyId).HasColumnName("i_OwnerCompanyId");
            entity.Property(e => e.i_SystemUserId).HasColumnName("i_SystemUserId");
            entity.Property(e => e.v_Process).HasColumnName("v_Process");
            entity.Property(e => e.i_Secuential).HasColumnName("i_Secuential");

        }
    }
}
