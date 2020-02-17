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
                                .HasName("PK_info");

            entity.ToTable("Info", "sunat");

            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
                .IsUnicode(false)
                .ValueGeneratedNever();

            entity.Property(e => e.CodigoZona)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Col)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CondicionDomicilio)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Departamento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.EstadoContribuyente)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Interior)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Kilometro)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Lote)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Manzana)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NombreVia)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RazonSocial)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.TipoVia)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.TipoZona)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Ubigeo)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
