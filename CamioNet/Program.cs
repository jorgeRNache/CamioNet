
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using camionet.Services;
using camionet.Services.SwaggerInputsFiltros;
using System.Text;
using System.Threading.RateLimiting;

namespace camionet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Configurar MySQL
            builder.Services.AddDbContext<CamioNetDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("conexion"))
            );


            // - Sistema para limitar las peticiones a un Controller
            builder.Services.AddRateLimiter(options =>
            {
                options.AddPolicy("querylimiter", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(httpContext.Connection.RemoteIpAddress?.ToString() ?? "global", _ =>
                        new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 1,  // Máximo 5 intentos permitidos
                            Window = TimeSpan.FromSeconds(5), // Cada 1 minuto
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 2 // Solo permite 2 solicitudes en espera
                        }));
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CamioNet API", Version = "v1.0" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Ingresar el token JWT",
                    Name = "Authorization",

                    Type = SecuritySchemeType.Http,
                    // Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            Array.Empty<string>()
                        }
                });

                // esto son como por asi decirlo modelos que tiene que seguir el swagger para que tenga ciertas apariencias
                // y poder personalizarlo mas
                c.OperationFilter<SwaggerFiltroFileFormParametro>();
                c.OperationFilter<SwaggerFiltroPasswordParametro>();


            });




            // - Token JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                              .AddJwtBearer(options =>
                              {
                                  options.TokenValidationParameters = new TokenValidationParameters
                                  {
                                      ValidateIssuer = true,
                                      ValidateAudience = true,
                                      ValidateLifetime = true,
                                      ValidateIssuerSigningKey = true,

                                      // Aquí agregas los valores de Issuer y Audience
                                      ValidIssuer = builder.Configuration["JwtSettings:Issuer"],  // Configurado en appsettings.json
                                      ValidAudience = builder.Configuration["JwtSettings:Audience"],  // Configurado en appsettings.json

                                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),

                                      // Esta propiedad ayuda a reducir la tolerancia al tiempo de caducidad de los tokens.
                                      ClockSkew = TimeSpan.Zero
                                  };
                              });


            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddScoped<PasswordHasher>();
            builder.Services.AddScoped<JwtTokenService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication(); // NECESARIO para que se valide el token JWT

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
