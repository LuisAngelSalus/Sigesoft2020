using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models.Win;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration.Win
{
    public class GroupOccupationWinConfiguration : IEntityTypeConfiguration<GroupOccupationWin>
    {
        public void Configure(EntityTypeBuilder<GroupOccupationWin> entity)
        {
            entity.HasKey(e => e.v_GroupOccupationId);
            entity.HasIndex(e => e.v_GroupOccupationId);
            entity.ToTable("groupoccupation", "dbo");

            entity.Property(e => e.v_GroupOccupationId).HasColumnName("v_GroupOccupationId");
            entity.Property(e => e.v_LocationId).HasColumnName("v_LocationId");
            entity.Property(e => e.v_Name).HasColumnName("v_Name");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

        }
    }
}
