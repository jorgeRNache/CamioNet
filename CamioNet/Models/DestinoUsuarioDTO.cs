using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace camionet.Models
{
    public class DestinoUsuarioDTO
    {
        [Key]
        public long destinousuariodto { get; set; }

        [ForeignKey("Destino")]
        public long destinoid { get; set; }
        public DestinoDTO Destino { get; set; } = null!;

        [ForeignKey("Usuario")]
        public long usuarioid { get; set; }
        public UsuarioDTO Usuario { get; set; } = null!;
    }
}
