using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data
{
    public class AccountSettingConfiguration : IEntityTypeConfiguration<AccountSetting>
    {
        public void Configure(EntityTypeBuilder<AccountSetting> entity)
        {

            entity.HasKey(e => e.i_AccountSettingId);

            entity.ToTable("AccountSetting", "common");

            entity.Property(e => e.i_AccountSettingId).HasColumnName("i_AccountSettingId");

            entity.Property(e => e.i_SystemUserId).HasColumnName("i_SystemUserId");

            entity.Property(e => e.i_OwnerCompanyId).HasColumnName("i_OwnerCompanyId");

            entity.Property(e => e.i_RoleId).HasColumnName("i_RoleId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

        }
    }
}
