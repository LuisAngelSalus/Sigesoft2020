using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class ProtocolWinConfiguration : IEntityTypeConfiguration<ProtocolWin>
    {
        public void Configure(EntityTypeBuilder<ProtocolWin> entity)
        {
            entity.HasKey(e => e.v_ProtocolId);
            entity.HasIndex(e => e.v_ProtocolId);
            entity.ToTable("protocol", "dbo");

            entity.Property(e => e.v_ProtocolId).HasColumnName("v_ProtocolId");
            entity.Property(e => e.v_Name).HasColumnName("v_Name");
            entity.Property(e => e.v_StatusProtocolId).HasColumnName("v_StatusProtocolId");
            entity.Property(e => e.v_EmployerOrganizationId).HasColumnName("v_EmployerOrganizationId");
            entity.Property(e => e.v_EmployerLocationId).HasColumnName("v_EmployerLocationId");
            entity.Property(e => e.i_EsoTypeId).HasColumnName("i_EsoTypeId");
            entity.Property(e => e.v_GroupOccupationId).HasColumnName("v_GroupOccupationId");
            entity.Property(e => e.v_CustomerOrganizationId).HasColumnName("v_CustomerOrganizationId");
            entity.Property(e => e.v_CustomerLocationId).HasColumnName("v_CustomerLocationId");
            entity.Property(e => e.v_WorkingOrganizationId).HasColumnName("v_WorkingOrganizationId");

            entity.Property(e => e.v_WorkingLocationId).HasColumnName("v_WorkingLocationId");            
            entity.Property(e => e.v_CostCenter).HasColumnName("v_CostCenter");
            entity.Property(e => e.i_MasterServiceTypeId).HasColumnName("i_MasterServiceTypeId");
            entity.Property(e => e.i_MasterServiceId).HasColumnName("i_MasterServiceId");
            entity.Property(e => e.i_HasVigency).HasColumnName("i_HasVigency");
            entity.Property(e => e.i_ValidInDays).HasColumnName("i_ValidInDays");
            entity.Property(e => e.i_IsActive).HasColumnName("i_IsActive");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
            entity.Property(e => e.i_ProfileId).HasColumnName("i_ProfileId");
            entity.Property(e => e.i_TypeReport).HasColumnName("i_TypeReport");

        }
    }
}
