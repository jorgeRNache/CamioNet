using System.ComponentModel.DataAnnotations;

namespace camionet.Models
{
    public enum Tipo
    {
        hola,
        adios
    }

    public class UsuarioDTO
    {
        [Key]
        public long usuarioid { get; set; }

        [Required, MaxLength(100)]
        public string contrasena { get; set; } = null!;

        [Required, MaxLength(100)]
        public string nombre_usuario { get; set; } = null!;

        [MaxLength(100)]
        public string nombre { get; set; } = null!;

        [MaxLength(100)]
        public string apellidos { get; set; } = null!;


        [Required]
        public int telefono { get; set; }

        public Tipo tipo_usuario { get; set;} = Tipo.hola;

        public DateTime fecha_alta { get; set; }

    }


    public class UsuarioLoginDTO
    {
        public string usuario { get; set; } = string.Empty;
        public int telefono { get; set; }
        public string contrasena { get; set; } = string.Empty;
    }

}
