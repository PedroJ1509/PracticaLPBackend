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
    public class UsuarioAplicacionConfiguracion : IEntityTypeConfiguration<UsuarioAplicacion>
    {
        public void Configure(EntityTypeBuilder<UsuarioAplicacion> builder)
        {
            builder.Property(x => x.Nombres).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Apellidos).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Clave).HasMaxLength(60).IsRequired(false);
        }
    }
}
