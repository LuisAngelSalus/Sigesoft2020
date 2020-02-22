using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> entity)
        {
            entity.HasKey(e => e.i_NotificationId);

            entity.ToTable("Notification", "common");

            entity.Property(e => e.i_NotificationId).HasColumnName("i_NotificationId");

            entity.Property(e => e.i_SystemUserId).HasColumnName("i_SystemUserId");

            entity.Property(e => e.v_Title).HasColumnName("v_Title");

            entity.Property(e => e.v_Message).HasColumnName("v_Message");

            entity.Property(e => e.i_PriorityLevelId).HasColumnName("i_PriorityLevelId");

            entity.Property(e => e.d_ShippingDate).HasColumnName("d_ShippingDate");

            entity.Property(e => e.i_WasRead).HasColumnName("i_WasRead");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.HasOne(d => d.SystemUser)
                .WithMany(p => p.Notification)
                .HasForeignKey(d => d.i_SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_SystemUser");

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
