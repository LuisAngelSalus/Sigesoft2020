using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class ClientUserConfiguration : IEntityTypeConfiguration<ClientUser>
    {
        public void Configure(EntityTypeBuilder<ClientUser> entity)
        {
            entity.HasKey(e => e.i_ClientUserId);

            entity.ToTable("ClientUser", "customer");

            entity.Property(e => e.i_ClientUserId).HasColumnName("i_ClientUserId");

            entity.Property(e => e.i_CompanyId).HasColumnName("i_CompanyId");

            entity.Property(e => e.v_UserName).HasColumnName("v_UserName");

            entity.Property(e => e.v_Password).HasColumnName("v_Password");

            entity.Property(e => e.v_FullName).HasColumnName("v_FullName");

            entity.Property(e => e.i_UserTypeId).HasColumnName("i_UserTypeId");

            entity.Property(e => e.i_TypeDocumentId).HasColumnName("i_TypeDocumentId");

            entity.Property(e => e.v_NroDocument).HasColumnName("v_NroDocument");

            entity.Property(e => e.v_NroCpm).HasColumnName("v_NroCpm");

            entity.Property(e => e.v_MobileNumber).HasColumnName("v_MobileNumber");

            entity.Property(e => e.v_Email).HasColumnName("v_Email");

            entity.Property(e => e.i_IsActive).HasColumnName("i_IsActive");

            entity.Property(e => e.i_IsDeleted).HasColumnName("i_IsDeleted");

            entity.Property(e => e.i_InsertUserId).HasColumnName("i_InsertUserId");

            entity.Property(e => e.d_InsertDate).HasColumnName("d_InsertDate");

            entity.Property(e => e.i_UpdateUserId).HasColumnName("i_UpdateUserId");

            entity.Property(e => e.d_UpdateDate).HasColumnName("d_UpdateDate");
        }
    }
}
