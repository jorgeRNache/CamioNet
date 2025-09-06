using System.ComponentModel.DataAnnotations;

namespace camionet.Models
{
    public class CamionDTO
    {
        [Key]
        public long camionid { get; set; }

        [Required, MaxLength(100)]
        public string nombre { get; set; } = null!;

        [MaxLength(500)]
        public string descripcion { get; set; } = null!;

    }
}
