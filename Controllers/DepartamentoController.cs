using MiApiUniversidad.Datos;
using MiApiUniversidad.Modelos;
using MiApiUniversidad.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MiApiUniversidad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly ILogger<DepartamentoController> _logger;
        private readonly ApplicationDbContext _db;

        public DepartamentoController(ILogger<DepartamentoController> logger, ApplicationDbContext db)
        {

            _logger = logger;
            _db = db;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]//Todo correcto
        public ActionResult <IEnumerable<DepartamentoDto>> GetDepartamentos()
        {
            _logger.LogInformation("Obtener los Departamentos");
            return Ok(_db.Departamentos.ToList());
        }


        [HttpGet("id:int", Name = "GetDepartamento")]
        ///CODIGOS DE ESTADO QUE SE MANEJAN //ENPOINTS DOCUMENTADOS
        [ProducesResponseType(StatusCodes.Status200OK)]//Todo correcto
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//
        [ProducesResponseType(StatusCodes.Status404NotFound)]//

        public ActionResult<DepartamentoDto> GetDepartamento(int id)
        {
            ////si el id es = 0 con el bad request retornara un error de 400
            if (id == 0)
            {
                _logger.LogError("Error al traer el departamento con Id " + id);
                return BadRequest();
            }

            //var departamento = DepartamentoStore.departamentoList.FirstOrDefault(d => d.Id == id);
            var departamento = _db.Departamentos.FirstOrDefault(v => v.Id == id);


            /// Not fount  se encuentra ningun registro error 404
            if (departamento == null)
            {
                return NotFound();
            }
            ///codigo de estado 200 todo correctamente
            return Ok(departamento);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]//Todo correcto
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]//

        public ActionResult<DepartamentoDto> CrearDepartamento([FromBody] DepartamentoDto departamentoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Departamentos.FirstOrDefault(v=>v.Nombre.ToLower() == departamentoDto.Nombre.ToLower()) !=null)
            {
                ModelState.AddModelError("NombreExiste", "El departamento con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            if(departamentoDto == null)
            {
                return BadRequest(departamentoDto);
            }
            if (departamentoDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //departamentoDto.Id = DepartamentoStore.departamentoList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //DepartamentoStore.departamentoList.Add(departamentoDto);

            Departamento modelo = new()
            {
                Nombre = departamentoDto.Nombre,
                Descripcion = departamentoDto.Descripcion
            };

            _db.Departamentos.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("GetDepartamento", new {id = departamentoDto.Id}, departamentoDto);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDepartamento(int id)
        {
            if (id==0)
            {
                return BadRequest();
            }
            var departamento = _db.Departamentos.FirstOrDefault(v => v.Id == id);
            if (departamento==null)
            {
                return NotFound();
            }
            //DepartamentoStore.departamentoList.Remove(departamento);
            _db.Departamentos.Remove(departamento);
            _db.SaveChanges();

            return NoContent();
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateDepartamento(int id, [FromBody] DepartamentoDto departamentoDto)
        {
            if (departamentoDto==null  || id!= departamentoDto.Id)
            {
                return BadRequest();
            }
            //var departamento = DepartamentoStore.departamentoList.FirstOrDefault(v => v.Id == id);
            //departamento.Nombre = departamentoDto.Nombre;
            //departamento.Descripcion = departamentoDto.Descripcion;

            Departamento modelo = new()
            {
                Id = departamentoDto.Id,
                Nombre = departamentoDto.Nombre,
                Descripcion = departamentoDto.Descripcion
            };
            _db.Departamentos.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialDepartamento(int id, JsonPatchDocument<DepartamentoDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            //var departamento = DepartamentoStore.departamentoList.FirstOrDefault(v => v.Id == id);

            var departamento = _db.Departamentos.AsNoTracking().FirstOrDefault(v => v.Id == id);

            DepartamentoDto departamentoDto = new()
            {
                Id = departamento.Id,
                Nombre = departamento.Nombre,
                Descripcion = departamento.Descripcion
            };

            if (departamento == null) return BadRequest();

            patchDto.ApplyTo(departamentoDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Departamento modelo = new()
            {
                Id = departamentoDto.Id,
                Nombre = departamentoDto.Nombre,
                Descripcion = departamentoDto.Descripcion
            };

            _db.Departamentos.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
