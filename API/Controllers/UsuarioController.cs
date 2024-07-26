using API.Helpers;
using Data;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.DTOs;
using Models.Entidades;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ITokenServicio _tokenServicio;
        private ApiResponse _response;
        private readonly RoleManager<RolAplicacion> _rolManager;

        public UsuarioController(UserManager<UsuarioAplicacion> userManager, ITokenServicio tokenServicio, RoleManager<RolAplicacion> rolManager)
        {
            _userManager = userManager;
            _tokenServicio = tokenServicio;
            _rolManager = rolManager;
            _response = new();
        }
        [Authorize(Policy = "AdminRol")]
        [HttpGet]
        public async Task<ActionResult> GetUsuarios()
        {
            var usuarios = await _userManager.Users.Select(u => new UsuarioListaDto()
            {
                Id = u.Id,
                Username = u.UserName,
                Apellidos = u.Apellidos,
                Nombres = u.Nombres,
                Email = u.Email//string.Join(",", _userManager.GetRolesAsync(u).Result.ToArray())
            }).ToListAsync();

            _response.Resultado = usuarios;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        [Authorize(Policy = "AdminRol")]
        [HttpGet("ListadoUsuario")]
        public async Task<ActionResult> GetListadoUsuario()
        {
            var usuariosUser = await _userManager.GetUsersInRoleAsync("Usuario");

            var usuarios = usuariosUser.Select(u => new UsuarioListaDto()
            {
                Id = u.Id,
                Username = u.UserName,
                Apellidos = u.Apellidos,
                Nombres = u.Nombres,
                Email = u.Email//string.Join(",", _userManager.GetRolesAsync(u).Result.ToArray())
            }).ToList();

            _response.Resultado = usuarios;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        [Authorize(Policy = "AdminUsuarioRol")]
        [HttpGet("GetUsuario/{username}")]
        public async Task<ActionResult> GetUsuario(string username)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username);

            _response.Resultado = usuario;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        //[Authorize]
        //[HttpGet("{id}")] //    api/usaurio/1
        //public async Task<ActionResult<Usuario>> GetUsuario(int id)
        //{
        //    var usuario = await _db.Usuarios.FindAsync(id);

        //    return Ok(usuario);
        //}
        [Authorize(Policy = "AdminRol")]
        [HttpPost("registro")]
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroUsuario registroDto)
        {
            if (await UsuarioExiste(registroDto.Username))
            {
                return BadRequest("No. Identificación ya existe");
            }

            //var username = await GenerateCadena(8);
            var clave = await GeneratePassword(8);

            var usuario = new UsuarioAplicacion
            {
                UserName = registroDto.Username.ToLower(),
                Email = registroDto.Email,
                Apellidos = registroDto.Apellidos,
                Nombres = registroDto.Nombres,
                Clave = EncryptionHelper.Encrypt(clave)
            };

            var resultado = await _userManager.CreateAsync(usuario, clave);
            if (!resultado.Succeeded) return BadRequest(resultado.Errors);

            //Asignar Rol a usuario
            var rolResultado = await _userManager.AddToRoleAsync(usuario, "Usuario");
            if (!rolResultado.Succeeded) return BadRequest("Error al agregar Rol al Usuario");

            _response.Resultado = usuario;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

            //return new UsuarioDto
            //{
            //    Username = usuario.UserName,
            //    Token = await _tokenServicio.CrearToken(usuario)
            //};
        }
        [HttpPost("login")] // api/usuario/login
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (usuario == null)
            {
                return Unauthorized("Usuario Invalido");//401
            }

            var resultado = await _userManager.CheckPasswordAsync(usuario, loginDto.Password);

            if (!resultado)
            {
                return Unauthorized("Contraseña Invalida");//401
            }

            return new UsuarioDto
            {
                Username = usuario.UserName,
                Token = await _tokenServicio.CrearToken(usuario)
            };
        }
        [Authorize(Policy = "AdminRol")]
        [HttpGet("ListadoRoles")]
        public IActionResult GetRoles()
        {
            var roles = _rolManager.Roles.Select(r => new { NombreRol = r.Name }).ToList();
            _response.Resultado = roles;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [Authorize(Policy = "AdminRol")]
        [HttpPut("EditarUsuario")]
        public async Task<ActionResult<UsuarioDto>> EditarUsuario(RegistroUsuario usuarioDto)
        {
            var usuarioExi = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == usuarioDto.Username);

            if (usuarioExi == null)
            {
                return BadRequest("Usuario No Existe en base de datos");//401
            }

            //usuarioExi.UserName = usuarioDto.Username.ToLower();
            usuarioExi.Email = usuarioDto.Email;
            usuarioExi.Apellidos = usuarioDto.Apellidos;
            usuarioExi.Nombres = usuarioDto.Nombres;
            

            await _userManager.UpdateAsync(usuarioExi);

            //await _userManager.RemoveFromRolesAsync(usuarioExi, await _userManager.GetRolesAsync(usuarioExi));

            ////Asignar Rol a usuario
            //var rolResultado = await _userManager.AddToRoleAsync(usuarioExi, usuarioDto.Rol);
            //if (!rolResultado.Succeeded) return BadRequest("Error al agregar Rol al Usuario");

            _response.Resultado = usuarioExi;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }



        [Authorize(Policy = "AdminRol")]
        [HttpPost("ObtenerDatosClave")]
        public async Task<ActionResult<UsuarioDto>> ObtenerDatosClave(RegistroUsuario usuarioDto)
        {
            var usuarioExi = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == usuarioDto.Username);

            if (usuarioExi == null)
            {
                return BadRequest("Usuario No Existe en base de datos");//401
            }

            var claveDesc = EncryptionHelper.Decrypt(usuarioExi.Clave);


            _response.Resultado = claveDesc;
            _response.IsExitoso = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }


        [Authorize(Policy = "AdminRol")]
        [HttpPost("GenerarClave")]
        public async Task<ActionResult<UsuarioDto>> GenerarClave(RegistroUsuario usuarioDto)
        {
            var usuarioExi = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == usuarioDto.Username);

            if (usuarioExi == null)
            {
                return BadRequest("Usuario No Existe en base de datos");//401
            }

            var clave = await GeneratePassword(8);
            var claveAnt = EncryptionHelper.Decrypt(usuarioExi.Clave);

            var claveEnc = EncryptionHelper.Encrypt(clave);

            usuarioExi.PasswordHash = _userManager.PasswordHasher.HashPassword(usuarioExi, clave);
            usuarioExi.Clave = claveEnc;

            var result = await _userManager.UpdateAsync(usuarioExi);

            if (result.Succeeded)
            {
                _response.Resultado = clave;
                _response.IsExitoso = true;
                _response.StatusCode = HttpStatusCode.OK;

            }
            else
            {
                _response.Resultado = "";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            return Ok(_response);

            //await _userManager.ChangePasswordAsync(usuarioExi, claveAnt, clave);

            //await _userManager.RemoveFromRolesAsync(usuarioExi, await _userManager.GetRolesAsync(usuarioExi));

            ////Asignar Rol a usuario
            //var rolResultado = await _userManager.AddToRoleAsync(usuarioExi, usuarioDto.Rol);
            //if (!rolResultado.Succeeded) return BadRequest("Error al agregar Rol al Usuario");

        }

        private async Task<bool> UsuarioExiste(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        private async Task<string> GeneratePassword(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@$?_-";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(validChars[rnd.Next(validChars.Length)]);
            }

            return res.ToString();
        }
        private async Task<string> GenerateCadena(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();

            while (0 < length--)
            {
                res.Append(validChars[rnd.Next(validChars.Length)]);
            }

            return res.ToString();
        }
    }
}
