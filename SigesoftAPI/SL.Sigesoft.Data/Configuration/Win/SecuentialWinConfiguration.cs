using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class SecuentialWinConfiguration : IEntityTypeConfiguration<SecuentialWin>
    {
        public void Configure(EntityTypeBuilder<SecuentialWin> entity)
        {
            entity.HasKey(e => new { e.i_NodeId, e.i_TableId})
              .HasName("PK_secuential");
            entity.ToTable("secuential", "dbo");

            entity.Property(e => e.i_NodeId).HasColumnName("i_NodeId");
            entity.Property(e => e.i_TableId).HasColumnName("i_TableId");
            entity.Property(e => e.i_SecuentialId).HasColumnName("i_SecuentialId");
        }
    }
}
