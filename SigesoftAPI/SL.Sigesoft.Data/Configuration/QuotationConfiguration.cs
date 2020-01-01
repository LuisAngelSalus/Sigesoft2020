using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class QuotationConfiguration : IEntityTypeConfiguration<Quotation>
    {
        public void Configure(EntityTypeBuilder<Quotation> entity)
        {
            entity.HasKey(e => e.i_QuotationId)
                .HasName("PK_quotation");

            entity.ToTable("Quotation", "commercial");
            entity.HasIndex(e => e.i_QuotationId);
            entity.Property(e => e.i_QuotationId).HasColumnName("i_QuotationId");

            entity.Property(e => e.v_Code).HasColumnName("v_Code");
            entity.Property(e => e.i_Version).HasColumnName("i_Version");
            entity.Property(e => e.i_UserCreatedId).HasColumnName("i_UserCreatedId");
            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");
            entity.Property(e => e.i_CompanyHeadquarterId).HasColumnName("i_CompanyHeadquarterId");
            entity.Property(e => e.v_FullName).HasColumnName("v_FullName");
            entity.Property(e => e.v_Email).HasColumnName("v_Email");

            entity.Property(e => e.i_TypeFormatId).HasColumnName("i_TypeFormatId");
            entity.Property(e => e.v_CommercialTerms).HasColumnName("v_CommercialTerms");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");
            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");
            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");
            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");
            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
            
            
        }
    }
}
