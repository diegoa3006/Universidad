using MiApiUniversidad.Datos;
using MiApiUniversidad.Modelos;
using MiApiUniversidad.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiApiUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly ILogger<MateriaController> _logger;
        private readonly ApplicationDbContext _db;
        public MateriaController(ILogger<MateriaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MateriaDto>> GetMaterias()
        {
            _logger.LogInformation("Obtener las materias");
            return Ok(_db.Materias.ToList());

        }

        [HttpGet("id:int", Name ="GetMateria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MateriaDto> GetMateria(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer la materia con Id " + id);
                return BadRequest();
            }

            //var materia = MateriaStore.materiaList.FirstOrDefault(v => v.Id == id);
            var materia = _db.Materias.FirstOrDefault(v => v.Id == id);

            if (materia == null)
            {
                return NotFound();
            }
            return Ok(materia);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MateriaDto> CrearMateria([FromBody] MateriaDto materiaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Materias.FirstOrDefault(v=>v.Nombre.ToLower() == materiaDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "La materia con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if (materiaDto ==null)
            {
                return BadRequest(materiaDto);
            }
            if (materiaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //materiaDto.Id = MateriaStore.materiaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //MateriaStore.materiaList.Add(materiaDto);
            Materia modelo = new()
            {
                Nombre = materiaDto.Nombre,
                Creditos = materiaDto.Creditos
            };
            _db.Materias.Add(modelo);
            _db.SaveChanges();
            return CreatedAtRoute("GetMateria", new {id = materiaDto.Id}, materiaDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMateria(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var materia = _db.Materias.FirstOrDefault(v => v.Id == id);
            if (materia == null)
            {
                return NotFound();
            }
            //MateriaStore.materiaList.Remove(materia);
            _db.Materias.Remove(materia);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMateria(int id, [FromBody] MateriaDto materiaDto)
        {
            if(materiaDto==null || id != materiaDto.Id)
            {
                return BadRequest();
            }
            //var materia = MateriaStore.materiaList.FirstOrDefault(v => v.Id == id);
            //materia.Nombre = materiaDto.Nombre;
            //materia.Creditos = materiaDto.Creditos;
            Materia modelo = new()
            {
                Id = materiaDto.Id,
                Nombre = materiaDto.Nombre,
                Creditos = materiaDto.Creditos
            };
            _db.Materias.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialMateria(int id, JsonPatchDocument<MateriaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var materia = MateriaStore.materiaList.FirstOrDefault(v => v.Id == id);
            var materia = _db.Materias.AsNoTracking().FirstOrDefault(v => v.Id == id);
            MateriaDto materiaDto = new()
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                Creditos = materia.Creditos
            };

            if (materia == null) return BadRequest();
            patchDto.ApplyTo(materiaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Materia modelo = new()
            {
                Id = materiaDto.Id,
                Nombre = materiaDto.Nombre,
                Creditos = materiaDto.Creditos
            };

            _db.Materias.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
