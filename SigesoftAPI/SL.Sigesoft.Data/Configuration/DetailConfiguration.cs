using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SL.Sigesoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SL.Sigesoft.Data.Configuration
{
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> entity)
        {
            entity.HasKey(e => e.i_DetailId)
                                .HasName("PK__Detail__151950163692F990");

            entity.ToTable("Detail", "sunat");

            entity.Property(e => e.i_DetailId).HasColumnName("i_DetailId");

            entity.Property(e => e.CodigoZona)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Col)
                .HasColumnName("col")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Departamento)
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

            entity.Property(e => e.Ruc)
                .HasMaxLength(11)
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
