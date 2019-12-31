using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class InfoConfiguration : IEntityTypeConfiguration<Info>
    {
        public void Configure(EntityTypeBuilder<Info> entity)
        {
            entity.HasKey(e => e.Ruc)
                    .HasName("PK_Info");

            entity.ToTable("Info", "sunat");

            entity.HasIndex(e => e.Ruc);
            entity.Property(e => e.Ruc).HasColumnName("Ruc");
            entity.Property(e => e.RazonSocial).HasColumnName("RazonSocial");
            entity.Property(e => e.EstadoContribuyente).HasColumnName("EstadoContribuyente");
            entity.Property(e => e.CondicionDomicilio).HasColumnName("CondicionDomicilio");
            entity.Property(e => e.Ubigeo).HasColumnName("Ubigeo");
            entity.Property(e => e.TipoVia).HasColumnName("TipoVia");
            entity.Property(e => e.NombreVia).HasColumnName("NombreVia");
            entity.Property(e => e.CodigoZona).HasColumnName("CodigoZona");
            entity.Property(e => e.TipoZona).HasColumnName("TipoZona");
            entity.Property(e => e.Numero).HasColumnName("Numero");
            entity.Property(e => e.Interior).HasColumnName("Interior");
            entity.Property(e => e.Lote).HasColumnName("Lote");
            entity.Property(e => e.Departamento).HasColumnName("Departamento");
            entity.Property(e => e.Manzana).HasColumnName("Manzana");
            entity.Property(e => e.Kilometro).HasColumnName("Kilometro");
            entity.Property(e => e.Col).HasColumnName("Col");


            entity.Property(e => e.Ruc)
            .HasColumnName("Ruc")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.RazonSocial)
            .HasColumnName("RazonSocial")
            .HasMaxLength(250)
            .IsUnicode(false);

            entity.Property(e => e.EstadoContribuyente)
            .HasColumnName("EstadoContribuyente")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.CondicionDomicilio)
            .HasColumnName("CondicionDomicilio")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Ubigeo)
            .HasColumnName("Ubigeo")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.TipoVia)
            .HasColumnName("TipoVia")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.NombreVia)
            .HasColumnName("NombreVia")
            .HasMaxLength(150)
            .IsUnicode(false);

            entity.Property(e => e.CodigoZona)
            .HasColumnName("CodigoZona")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.TipoZona)
            .HasColumnName("TipoZona")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Numero)
            .HasColumnName("Numero")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Interior)
            .HasColumnName("Interior")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Lote)
            .HasColumnName("Lote")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Departamento)
            .HasColumnName("Departamento")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Manzana)
            .HasColumnName("Manzana")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Kilometro)
            .HasColumnName("Kilometro")
            .HasMaxLength(50)
            .IsUnicode(false);

            entity.Property(e => e.Col)
            .HasColumnName("Col")
            .HasMaxLength(50)
            .IsUnicode(false);

        }
    }
}
