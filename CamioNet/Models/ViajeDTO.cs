using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace camionet.Models
{
    public class ViajeDTO
    {
        [Key]
        public long viajeid { get; set; }

        [ForeignKey("Camion")]
        public long camionid { get; set; }
        public CamionDTO Camion { get; set; } = null!;


        [ForeignKey("Usuario_Conductor")]
        public long usuarioid_conductor { get; set; }
        public UsuarioDTO Usuario_Conductor { get; set; } = null!;


        [ForeignKey("Usuario_Cliente")]
        public long usuarioid_cliente { get; set; }
        public UsuarioDTO Usuario_Cliente { get; set; } = null!;


        [ForeignKey("Destino")]
        public long destinoid { get; set; }
        public DestinoDTO Destino { get; set; } = null!;


        public string descripcion { get; set; } = null!;


        public DateTime fecha_hora { get; set; }

    }
}
