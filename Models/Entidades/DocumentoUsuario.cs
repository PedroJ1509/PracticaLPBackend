using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class DocumentoUsuario
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Paciente es Requerido")]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public UsuarioAplicacion Usuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
