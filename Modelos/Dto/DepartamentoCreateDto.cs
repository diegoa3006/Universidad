using System.ComponentModel.DataAnnotations;

namespace MiApiUniversidad.Modelos.Dto
{
    public class DepartamentoCreateDto
    {
        [Required]
        [MaxLength(30)] //MAXIMO DE 30 CARACTERES
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set;}
    }
}
