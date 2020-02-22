using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class SuscriptionConfiguration : IEntityTypeConfiguration<Suscription>
    {
        public void Configure(EntityTypeBuilder<Suscription> entity)
        {
            entity.HasKey(e => e.i_SuscriptionId);

            entity.ToTable("Suscription", "common");

            entity.Property(e => e.i_SuscriptionId).HasColumnName("i_SuscriptionId");

            entity.Property(e => e.v_Body).HasColumnName("v_Body");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.HasOne(d => d.SystemUser)
                .WithMany(p => p.Suscription)
                .HasForeignKey(d => d.i_SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Suscription_SystemUser");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
