using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.DataClass;
using Proyecto_API_MHW.Models;
using System.Text.RegularExpressions;

namespace Proyecto_API_MHW.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController: ControllerBase
    {

        private readonly UsuarioContext UsuarioApi;

        public UsuarioController(UsuarioContext context)
        {
            this.UsuarioApi = context;
        }

        // Regex simple similar al CHECK en la DB
        private static readonly Regex EmailRegex = new(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [HttpPost("nuevo")]
        public async Task<ActionResult<Usuario>> InsertarUsuario(DtoRegister usuario)
        {
            // Validaciones básicas de entrada (DtoRegister tiene DataAnnotations)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(usuario.usuario) ||
                string.IsNullOrWhiteSpace(usuario.password) ||
                string.IsNullOrWhiteSpace(usuario.correo) ||
                string.IsNullOrWhiteSpace(usuario.nombre) ||
                string.IsNullOrWhiteSpace(usuario.apellido))
            {
                return BadRequest("Todos los campos son obligatorios");
            }

            if (usuario.password.Length < 8)
            {
                return BadRequest("La contraseña debe tener al menos 8 caracteres");
            }

            if (!EmailRegex.IsMatch(usuario.correo))
            {
                return BadRequest("El formato del correo no es válido");
            }

            // Comprobar unicidad de usuario y correo
            if (await UsuarioApi.Usuarios.AnyAsync(u => u.Nombreusuario == usuario.usuario))
            {
                return StatusCode(409, "Existe Usuario con ese nombre");
            }

            if (await UsuarioApi.Usuarios.AnyAsync(u => u.Correo == usuario.correo))
            {
                return StatusCode(409, "El correo ya está en uso");
            }

            UsuarioApi.Usuarios.Add(new Usuario()
            {
                Nombreusuario = usuario.usuario,
                Password = usuario.password,
                Correo = usuario.correo,
                Nombre = usuario.nombre,
                Apellido = usuario.apellido
            });

            await UsuarioApi.SaveChangesAsync();
            return StatusCode(201, "Se Registro nuevo usuario");
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> BuscarUsuario(DtoLogin usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Comprobación simple de credenciales
            var exists = await UsuarioApi.Usuarios.AnyAsync(u =>
                u.Nombreusuario == usuario.usuario && u.Password == usuario.password);

            if (exists)
            {
                return Ok();
            }

            return StatusCode(401, "Usuario no registrado");
        }
    }
}
