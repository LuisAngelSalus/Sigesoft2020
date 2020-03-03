using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class OrganizationWinConfiguration : IEntityTypeConfiguration<OrganizationWin>
    {
        public void Configure(EntityTypeBuilder<OrganizationWin> entity)
        {
            entity.HasKey(e => e.v_OrganizationId);
            entity.HasIndex(e => e.v_OrganizationId);
            entity.ToTable("organization", "dbo");

            entity.Property(e => e.v_OrganizationPadreId).HasColumnName("v_OrganizationPadreId");
            entity.Property(e => e.i_OrganizationTypeId).HasColumnName("i_OrganizationTypeId");
            entity.Property(e => e.i_SectorTypeId).HasColumnName("i_SectorTypeId");
            entity.Property(e => e.v_IdentificationNumber).HasColumnName("v_IdentificationNumber");
            entity.Property(e => e.v_Name).HasColumnName("v_Name");
            entity.Property(e => e.v_Address).HasColumnName("v_Address");
            entity.Property(e => e.v_PhoneNumber).HasColumnName("v_PhoneNumber");
            entity.Property(e => e.v_Mail).HasColumnName("v_Mail");
            entity.Property(e => e.v_ContacName).HasColumnName("v_ContacName");
            entity.Property(e => e.v_Observation).HasColumnName("v_Observation");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

        }
}
}
