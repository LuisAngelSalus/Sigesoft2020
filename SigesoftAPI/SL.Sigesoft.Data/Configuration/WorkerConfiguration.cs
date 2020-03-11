using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> entity)
        {
            entity.HasKey(e => e.i_WorkerId);

            entity.ToTable("Worker", "medical");

            entity.Property(e => e.i_WorkerId).HasColumnName("i_WorkerId");            

            entity.Property(e => e.i_PersonId).HasColumnName("i_PersonId");

            entity.Property(e => e.v_CurrentPosition).HasColumnName("v_CurrentPosition");

            entity.Property(e => e.v_HomeAddress).HasColumnName("v_HomeAddress");

            entity.Property(e => e.d_DateOfBirth).HasColumnName("d_DateOfBirth");

            entity.Property(e => e.i_GenderId).HasColumnName("i_GenderId");

            entity.Property(e => e.v_Email).HasColumnName("v_Email");

            entity.Property(e => e.v_MobileNumber).HasColumnName("v_MobileNumber");

            entity.Property(e => e.i_TypeDocumentId).HasColumnName("i_TypeDocumentId");

            entity.Property(e => e.v_NroDocument).HasColumnName("v_NroDocument");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
        }
    }
}
