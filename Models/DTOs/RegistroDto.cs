using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RegistroDto
    {
        [Required(ErrorMessage ="Username es Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password es Requerido")]
        [StringLength(8,MinimumLength =4,ErrorMessage = "El password debe ser mínimo 4 y máximo 10 carateres")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Apellidos es Requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Nombres es Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Email es Requerido")]
        public string Email { get; set; }
    }
}
