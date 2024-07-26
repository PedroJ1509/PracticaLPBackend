using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class TipoDocumento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Descripción Clinica es Requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "La Descripción debe de ser minimo 1 Maximo 50 caracteres")]
        public string Descripcion { get; set; }
    }
}
