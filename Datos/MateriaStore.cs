using MiApiUniversidad.Modelos.Dto;

namespace MiApiUniversidad.Datos
{
    public static class MateriaStore
    {
        public static List<MateriaDto> materiaList = new List<MateriaDto>
        {
            new MateriaDto{Id=1, Nombre="Creditos 1", Creditos=3},
            new MateriaDto{Id=2, Nombre="Creditos 2", Creditos=4}

        };
    }
}
