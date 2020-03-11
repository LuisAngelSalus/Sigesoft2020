﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> entity)
        {
            entity.HasKey(e => e.i_ScheduleId);

            entity.ToTable("Schedule", "Medical");

            entity.Property(e => e.i_ScheduleId).HasColumnName("i_ScheduleId");

            entity.Property(e => e.i_WorkerId).HasColumnName("i_WorkerId");

            entity.Property(e => e.i_ServiceId).HasColumnName("i_ServiceId");

            entity.Property(e => e.d_DateTimeCalendar).HasColumnName("d_DateTimeCalendar");

            entity.Property(e => e.d_CircuitStartDate).HasColumnName("d_CircuitStartDate");

            entity.Property(e => e.d_EntryTimeCM).HasColumnName("d_EntryTimeCM");

            entity.Property(e => e.i_ServiceTypeId).HasColumnName("i_ServiceTypeId");

            entity.Property(e => e.i_CalendarStatusId).HasColumnName("i_CalendarStatusId");

            entity.Property(e => e.i_ServiceModeId).HasColumnName("i_ServiceModeId");

            entity.Property(e => e.i_ProtocolId).HasColumnName("i_ProtocolId");

            entity.Property(e => e.i_NewContinuationId).HasColumnName("i_NewContinuationId");

            entity.Property(e => e.i_LineStatusId).HasColumnName("i_LineStatusId");

            entity.Property(e => e.i_IsVipId).HasColumnName("i_IsVipId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");


        }
    }
}
