using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.HasKey(e => e.i_PersonId)
                                .HasName("PK_person");

            entity.ToTable("Person", "security");

            entity.Property(e => e.i_PersonId).HasColumnName("i_PersonId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.v_FirstLastName)
                .IsRequired()
                .HasColumnName("v_FirstLastName")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.v_FirstName)
                .IsRequired()
                .HasColumnName("v_FirstName")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.v_SecondLastName)
                .HasColumnName("v_SecondLastName")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasQueryFilter(x => x.i_IsDeleted == Models.Enum.YesNo.No);
        }
    }
}
