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
            entity.HasKey(e => e.i_SecuentialId);

            entity.ToTable("Secuential", "common");

            entity.Property(e => e.i_SecuentialId).HasColumnName("i_SecuentialId");

            entity.Property(e => e.i_OwnerCompanyId).HasColumnName("i_OwnerCompanyId");

            entity.Property(e => e.i_Secuential).HasColumnName("i_Secuential");

            entity.Property(e => e.i_SystemUserId).HasColumnName("i_SystemUserId");

            entity.Property(e => e.v_Process)
                .IsRequired()
                .HasColumnName("v_Process")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.OwnerCompany)
                .WithMany(p => p.Secuential)
                .HasForeignKey(d => d.i_OwnerCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Secuential_OwnerCompany");

            entity.HasOne(d => d.SystemUser)
                .WithMany(p => p.Secuential)
                .HasForeignKey(d => d.i_SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Secuential_SystemUser");

        }
    }
}
