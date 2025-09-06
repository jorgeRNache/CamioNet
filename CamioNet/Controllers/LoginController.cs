using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using camionet.Models;
using camionet.Services;
using Org.BouncyCastle.Crypto.Generators;

namespace camionet.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        // - Declaracion de las propiedades de conexion
        private readonly IConfiguration _configuration;
        private readonly JwtTokenService _jwtTokenService;
        private CamioNetDbContext _nexPayDbContext;
        private readonly PasswordHasher _passwordHasher;

        public LoginController(CamioNetDbContext context, PasswordHasher passwordHasher, JwtTokenService jwtTokenService)
        {
            _nexPayDbContext = context;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }


        [HttpPost("Auth")]
        [EnableRateLimiting("querylimiter")]
        public async Task<IActionResult> AuthMethod()
        {
            try
            {
                string res = "";

                //if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
                //{
                //    return BadRequest("Credenciales inválidas.");
                //}
                //else
                //{
                //    // - Encryptamos la pass
                //    var passwordHasher = _passwordHasher.HashPassword(login.Password);


                //    // - Buscar usuario por nombre y su pass
                //    var user = await _nexPayDbContext.Login.FirstOrDefaultAsync(u => u.Username == login.Username && u.Password == passwordHasher);

                //    if (user == null)
                //    {
                //        return Unauthorized("Usuario o contraseña incorrectos.");
                //    }




                //    // Generar token
                //    var token = _jwtTokenService.GenerateAccessToken(user.Username);

                //    res = token;
                //}

                return Ok(res);
            }
            catch (Exception ex)
            {
                // Puedes loguear el error aquí si usas ILogger
                return StatusCode(500, "Error interno del servidor.");
            }
        }

    }
}
