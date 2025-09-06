using System.ComponentModel.DataAnnotations;

namespace camionet.Models
{
    public class DestinoDTO
    {
        [Key]
        public long destinoid { get; set; }

        [Required, MaxLength(100)]
        public string direccion { get; set; } = null!;

        [MaxLength(500)]
        public string descripcion { get; set; } = null!;
    }
}
