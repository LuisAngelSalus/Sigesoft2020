﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class CompanyHeadquarterConfiguration : IEntityTypeConfiguration<CompanyHeadquarter>
    {
        public void Configure(EntityTypeBuilder<CompanyHeadquarter> entity)
        {
            entity.HasKey(e => e.i_CompanyHeadquarterId)
                    .HasName("PK_companyHeadquarter");

            entity.ToTable("CompanyHeadquarter", "commercial");

            entity.Property(e => e.i_CompanyHeadquarterId).HasColumnName("i_CompanyHeadquarterId");
            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");
            entity.Property(e => e.v_Name).HasColumnName("v_Name");
            entity.Property(e => e.v_Address).HasColumnName("v_Address");
            entity.Property(e => e.v_PhoneNumber).HasColumnName("v_PhoneNumber");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.v_Name)
               .HasColumnName("v_Name")
               .HasMaxLength(50)
               .IsUnicode(false);

            entity.Property(e => e.v_Address)
               .HasColumnName("v_Address")
               .HasMaxLength(150)
               .IsUnicode(false);

            entity.Property(e => e.v_PhoneNumber)
               .HasColumnName("v_PhoneNumber")
               .HasMaxLength(100)
               .IsUnicode(false);

            //entity.HasOne(d => d.Company)
            //.WithMany(p => p.)
            //.HasForeignKey(d => d.i_CompanyId)
            //.HasConstraintName("FK_CompanyHeadquarter_Company");


        }
    }
}
