using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UsuarioDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
