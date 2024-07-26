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
    public class DocumentoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Descripción Clinica es Requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "La Descripción debe de ser minimo 1 Maximo 50 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Usuario es Requerido")]
        public Guid DocumentoUsaurioId { get; set; }
        [Required(ErrorMessage = "Observacion Clinica es Requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La Url debe de ser minimo 1 Maximo 300 caracteres")]
        public string url { get; set; }
        [Required(ErrorMessage = "TipoDocumento es Requerido")]
        public int TipoDocumentoId { get; set; }
        public string tipoDocumento { get; set; }
        public string FechaCreacion { get; set; }
    }
}
