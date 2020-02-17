using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ProtocolConfiguration : IEntityTypeConfiguration<Protocol>
    {
        public void Configure(EntityTypeBuilder<Protocol> entity)
        {
            entity.HasKey(e => e.i_ProtocolId)
               .HasName("PK_Protocol");

            entity.ToTable("Protocol", "protocol");
            entity.HasIndex(e => e.i_ProtocolId);

            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");
            entity.Property(e => e.v_ProtocolName).HasColumnName("v_ProtocolName");
            entity.Property(e => e.i_ServiceTypeId).HasColumnName("i_ServiceTypeId");
            entity.Property(e => e.i_TypeFormatId).HasColumnName("i_TypeFormatId");
            entity.Property(e => e.i_QuotationProfileIdRef).HasColumnName("i_QuotationProfileIdRef");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.v_ProtocolName)
              .HasColumnName("v_ProtocolName")
              .HasMaxLength(150)
              .IsUnicode(false);
        }
    }
}
