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
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioContext UsuarioApi;

        public UsuarioController(UsuarioContext context)
        {
            this.UsuarioApi = context;
        }

        // Regex simple similar al CHECK en la DB
        private static readonly Regex EmailRegex = new(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [HttpPost("nuevo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Usuario>> InsertarUsuario(DtoRegister usuario)
        {
            // Validaciones básicas (si no usas [ApiController], esto es útil)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Normalización básica
            var nombreUsuario = usuario.usuario?.Trim();
            var correo = usuario.correo?.Trim();
            var nombre = usuario.nombre?.Trim();
            var apellido = usuario.apellido?.Trim();
            var password = usuario.password; // puede requerir reglas adicionales

            // Campos requeridos
            if (string.IsNullOrWhiteSpace(nombreUsuario) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(correo) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(apellido))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            // Longitud mínima contraseña
            if (password.Length < 8)
                return BadRequest("La contraseña debe tener al menos 8 caracteres.");

            // Validación correcta de correo: RECHAZAR si NO coincide
            if (!EmailRegex.IsMatch(correo))
                return BadRequest("El formato del correo no es válido.");

            // Verificar existencia (await, sin .Result)
            // Comparación case-insensitive para usuario/correo

            var existeUsuario = await UsuarioApi.Usuarios.AnyAsync(u => u.Nombreusuario == nombreUsuario);
            var existeCorreo = await UsuarioApi.Usuarios.AnyAsync(u => u.Correo == correo);

            // RECOMENDACIÓN: Hashear contraseña antes de guardar (ejemplo ficticio)
            // var passwordHash = _passwordHasher.Hash(password);

            var nuevoUsuario = new Usuario
            {
                Nombreusuario = nombreUsuario,
                Password = password,      // <-- Sustituye por 'passwordHash' cuando lo implementes
                Correo = correo,
                Nombre = nombre,
                Apellido = apellido
            };

            UsuarioApi.Usuarios.Add(nuevoUsuario);
            await UsuarioApi.SaveChangesAsync(); // <-- IMPORTANTE: await

            // Si tienes un endpoint GET para detalle, usa CreatedAtAction:
            // return CreatedAtAction(nameof(GetUsuarioBy    // return CreatedAtAction(nameof(GetUsuarioById), new { id = nuevoUsuario.IdUsuario }, nuevoUsuario);

            // Si aún no lo tienes, devolver 201 con el recurso creado es suficiente:
            return StatusCode(StatusCodes.Status201Created, nuevoUsuario);

        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Usuario>> BuscarUsuario(DtoLogin usuario)
        {
            // 1) Validación de modelo (si no usas [ApiController], esto es útil)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2) Normalizar entrada y proteger nulos (evita CS8602)
            var nombreUsuario = usuario.usuario?.Trim();
            var password = usuario.password?.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(password))
                return BadRequest("Usuario y contraseña son obligatorios.");

            // 3) Igualdad exacta (NO usar StartsWith en contraseñas)
            //    Si tu comparación de usuario debe ser case-insensitive, ver alternativas abajo.
            var existe = await UsuarioApi.Usuarios
                .AnyAsync(u => u.Nombreusuario == nombreUsuario && u.Password == password);

            if (!existe)
                return Unauthorized("Usuario no registrado");

            return Ok();

        }
    }
}
