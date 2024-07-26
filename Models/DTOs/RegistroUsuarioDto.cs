using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class RegistroUsuario
    {
        [Required(ErrorMessage = "No. Identificación es Requerido")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Apellidos es Requerido")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Nombres es Requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Email es Requerido")]
        public string Email { get; set; }
    }
}
