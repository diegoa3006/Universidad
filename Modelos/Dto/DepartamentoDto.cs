using System.ComponentModel.DataAnnotations;

namespace MiApiUniversidad.Modelos.Dto
{
    public class DepartamentoDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)] //MAXIMO DE 30 CARACTERES
        public string Nombre { get; set; }
        public string Descripcion { get; set;}
    }
}
