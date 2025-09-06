using camionet.Models;
using Microsoft.EntityFrameworkCore;

namespace camionet.Services
{
    public class CamioNetDbContext : DbContext
    {
        public CamioNetDbContext(DbContextOptions<CamioNetDbContext> options)
           : base(options) { }

        public DbSet<UsuarioDTO> Usuario { get; set; } = null!;
        public DbSet<CamionDTO> Camion { get; set; } = null!;
        public DbSet<DestinoDTO> Destino { get; set; } = null!;
        public DbSet<DestinoUsuarioDTO> DestinoUsuario { get; set; } = null!;
        public DbSet<ViajeDTO> Viaje { get; set; } = null!;


    }
}
