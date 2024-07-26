using Models.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class DocumentoUsuarioDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Paciente es Requerido")]
        public int UsuarioId { get; set; }
    }
}
