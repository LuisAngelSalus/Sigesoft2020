using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class UbigeoConfiguration : IEntityTypeConfiguration<Ubigeo>
    {
        public void Configure(EntityTypeBuilder<Ubigeo> entity)
        {
            entity.HasKey(e => e.i_UbigeoId);

            entity.ToTable("Ubigeo", "sunat");

            entity.Property(e => e.i_UbigeoId).HasColumnName("i_UbigeoId");
            entity.Property(e => e.v_Departamento).HasColumnName("v_Departamento");
            entity.Property(e => e.v_Provincia).HasColumnName("v_Provincia");
            entity.Property(e => e.v_Distrito).HasColumnName("v_Distrito");
            entity.Property(e => e.v_Ubigeo).HasColumnName("v_Ubigeo");
        }

    }
}
