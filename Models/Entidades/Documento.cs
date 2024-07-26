using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entidades
{
    public class Documento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Descripción Clinica es Requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "La Descripción debe de ser minimo 1 Maximo 50 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Historia Clinica es Requerido")]
        public Guid DocumentoUsaurioId { get; set; }
        [ForeignKey("DocumentoUsaurioId")]
        public DocumentoUsuario DocumentoUsuario { get; set; }
        [Required(ErrorMessage = "Observacion Clinica es Requerido")]
        [StringLength(100, MinimumLength = 1 , ErrorMessage = "La Url debe de ser minimo 1 Maximo 300 caracteres")]
        public string url { get; set; }
        [Required(ErrorMessage = "TipoDocumento es Requerido")]
        public int TipoDocumentoId { get; set; }
        [ForeignKey("TipoDocumentoId")]
        public TipoDocumento tipoDocumento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        [Display(Name = "Creado Por")]
        public int? CreadoPorId { get; set; }
        [ForeignKey("CreadoPorId")]
        public UsuarioAplicacion CreadoPor { get; set; }
        [Display(Name = "Actualizado Por")]
        public int? ActualizadoPorId { get; set; }
        [ForeignKey("ActualizadoPorId")]
        public UsuarioAplicacion ActualizadoPor { get; set; }
    }
}
