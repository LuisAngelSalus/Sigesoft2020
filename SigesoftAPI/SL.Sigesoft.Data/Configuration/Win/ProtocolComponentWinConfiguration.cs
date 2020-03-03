using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class ProtocolComponentWinConfiguration : IEntityTypeConfiguration<ProtocolComponentWin>
    {
        public void Configure(EntityTypeBuilder<ProtocolComponentWin> entity)
        {
            
            entity.HasKey(e => e.v_ProtocolComponentId);
            entity.HasIndex(e => e.v_ProtocolComponentId);
            entity.ToTable("protocolcomponent", "dbo");
            entity.Property(e => e.v_ProtocolComponentId).HasColumnName("v_ProtocolComponentId");
            entity.Property(e => e.v_ProtocolId).HasColumnName("v_ProtocolId");
            entity.Property(e => e.v_ComponentId).HasColumnName("v_ComponentId");
            entity.Property(e => e.r_Price).HasColumnName("r_Price");
            entity.Property(e => e.i_OperatorId).HasColumnName("i_OperatorId");
            entity.Property(e => e.i_Age).HasColumnName("i_Age");
            entity.Property(e => e.i_GenderId).HasColumnName("i_GenderId");
            entity.Property(e => e.i_IsConditionalId).HasColumnName("i_IsConditionalId");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
        }
    }
}
