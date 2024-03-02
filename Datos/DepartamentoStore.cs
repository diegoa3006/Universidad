using MiApiUniversidad.Modelos.Dto;

namespace MiApiUniversidad.Datos
{
    public static class DepartamentoStore
    {
        public static List<DepartamentoDto> departamentoList = new List<DepartamentoDto>
        {
            new DepartamentoDto{Id=1, Nombre="Sistemas", Descripcion="Sistemas e Informatica"},
            new DepartamentoDto{Id=2, Nombre="Contaduria", Descripcion="Samuel Silverio"}

        };
    }
}
