using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_API_MHW.Contexts;
using Proyecto_API_MHW.DataClass;
using Proyecto_API_MHW.Models;

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

        [HttpPost("nuevo")]
        public async Task<ActionResult<Usuario>> InsertarUsuario(DtoUsuario usuario)
        {
            bool UserExist = false;
            await UsuarioApi.Usuarios.ForEachAsync(u =>
            {
                if (u.Nombreusuario == usuario.usuario)
                {
                    UserExist = true;
                }
            });
            if (UserExist)
            {
                return StatusCode(409, "Existe Usuario con ese nombre");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UsuarioApi.Usuarios.Add(new Usuario()
            {
                Nombreusuario = usuario.usuario,
                Password = usuario.password
            });
            await UsuarioApi.SaveChangesAsync();
            return StatusCode(201, "Se Registro nuevo usuario");
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> BuscarUsuario(DtoUsuario usuario)
        {
            bool UserExist = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await UsuarioApi.Usuarios.ForEachAsync(u =>
            {
                if (u.Nombreusuario == usuario.usuario && u.Password == usuario.password)
                {
                    UserExist = true;
                }
            });
            if (UserExist)
            {
                return Ok();
            }
            return StatusCode(401, "Usuario no registrado");
        }
    }
}
