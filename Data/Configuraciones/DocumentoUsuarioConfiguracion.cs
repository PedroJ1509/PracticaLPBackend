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
    public class DocumentoUsuarioConfiguracion : IEntityTypeConfiguration<DocumentoUsuario>
    {
        public void Configure(EntityTypeBuilder<DocumentoUsuario> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UsuarioId).IsRequired();

            /*Relaciones*/

            builder.HasOne(x => x.Usuario)
                    .WithOne(x => x.DocumentoUsuario)
                    .HasForeignKey<DocumentoUsuario>(x => x.UsuarioId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
