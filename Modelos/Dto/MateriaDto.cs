using System.ComponentModel.DataAnnotations;

namespace MiApiUniversidad.Modelos.Dto
{
    public class MateriaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public int Creditos { get; set; }

    }
}
