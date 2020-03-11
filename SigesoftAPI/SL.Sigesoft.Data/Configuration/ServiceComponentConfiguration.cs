using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ServiceComponentConfiguration : IEntityTypeConfiguration<ServiceComponent>
    {
        public void Configure(EntityTypeBuilder<ServiceComponent> entity)
        {
            entity.HasKey(e => e.i_ServiceComponentId);

            entity.ToTable("ServiceComponent", "Medical");

            entity.Property(e => e.i_ServiceComponentId).HasColumnName("i_ServiceComponentId");

            entity.Property(e => e.i_ServiceId).HasColumnName("i_ServiceId");

            entity.Property(e => e.v_ComponentId).HasColumnName("v_ComponentId");

            entity.Property(e => e.i_ServiceComponentStatusId).HasColumnName("i_ServiceComponentStatusId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");


        }
    }
}
