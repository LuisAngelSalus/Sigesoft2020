using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> entity)
        {
            entity.HasKey(e => e.i_ServiceId);

            entity.ToTable("Service", "Medical");

            entity.Property(e => e.i_ServiceId).HasColumnName("i_ServiceId");

            entity.Property(e => e.i_ProtocolId).HasColumnName("i_ProtocolId");

            entity.Property(e => e.i_WorkerId).HasColumnName("i_WorkerId");

            entity.Property(e => e.i_ServiceStatusId).HasColumnName("i_ServiceStatusId");

            entity.Property(e => e.i_AptitudeStatusId).HasColumnName("i_AptitudeStatusId");

            entity.Property(e => e.d_ServiceDate).HasColumnName("d_ServiceDate");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

        }
    }
}
