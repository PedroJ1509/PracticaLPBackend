using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configuraciones
{
    public class DocumentoConfiguracion : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Descripcion).HasMaxLength(60).IsRequired();
            builder.Property(x => x.url).HasMaxLength(60).IsRequired();
            builder.Property(x => x.CreadoPorId).IsRequired(false);
            builder.Property(x => x.ActualizadoPorId).IsRequired(false);

            /*Relaciones*/
            builder.HasOne(x => x.CreadoPor)
                    .WithMany()
                    .HasForeignKey(x => x.CreadoPorId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ActualizadoPor).WithMany()
                    .HasForeignKey(x => x.ActualizadoPorId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
