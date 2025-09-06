using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using camionet.Models;
using camionet.Services;
using Org.BouncyCastle.Crypto.Generators;

namespace camionet.Controllers
{
    [ApiController]
    [Route("api/Usuarios")]
    public class UsuarioController : ControllerBase
    {

        private readonly CamioNetDbContext _camioNetDbContext;

        public UsuarioController(CamioNetDbContext context)
        {
            _camioNetDbContext = context;
        }

        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CrearNuevoUsuario([FromBody] UsuarioDTO nuevoUsuario)
        {
            try
            {
                if ((string.IsNullOrWhiteSpace(nuevoUsuario.nombre_usuario) || nuevoUsuario.telefono==0) || string.IsNullOrWhiteSpace(nuevoUsuario.contrasena))
                {
                    return BadRequest("Completo los campos de inicio de sesion necesarios.");
                }

                // Verificar si ya existe un usuario con ese email
                bool emailExiste = await _camioNetDbContext.Usuario.AnyAsync(u => u.nombre_usuario == nuevoUsuario.nombre_usuario || u.telefono == nuevoUsuario.telefono);
                if (emailExiste)
                {
                    return Conflict("Usuario ya registrado.");
                }

                // Hash de la contraseña
                string hashedPassword = PasswordHasher.HashPassword(nuevoUsuario.contrasena);
                nuevoUsuario.contrasena = hashedPassword;
         


                // Agregar usuario a la base de datos
                await _camioNetDbContext.Usuario.AddAsync(nuevoUsuario);
                await _camioNetDbContext.SaveChangesAsync();

                return Ok(new { message = "Usuario y login creados correctamente" });
            }
            catch (Exception ex)
            {
                // Log error si tienes ILogger (recomendado)
                return StatusCode(500, "Error interno al crear el usuario");
            }
        }



    }
}
