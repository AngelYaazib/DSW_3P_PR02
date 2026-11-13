using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR02.Models;
using DSW_3P_PR02.Services;

namespace DSW_3P_PR02.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _service;

    public EventoController(IEventoService service)
    {
        _service = service;
    }

    // GET: api/eventos
    [HttpGet]
    public ActionResult<List<Evento>> ObtenerTodas()
    {
        return Ok(_service.ObtenerTodas());
    }

    // GET: api/eventos/1
    [HttpGet("{id:int}")]
    public ActionResult<Evento> ObtenerPorId(int id)
    {
        var evento = _service.ObtenerPorId(id);

        if (evento == null)
            return NotFound(new { mensaje = "El evento no existe." });

        return Ok(evento);
    }

    // POST: api/eventos
    [HttpPost]
    public ActionResult<Evento> Agregar([FromBody] Evento evento)
    {
        try
        {
            var nuevoEvento = _service.Agregar(evento);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = nuevoEvento.Id },
                nuevoEvento
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // PUT: api/eventos/1
    [HttpPut("{id:int}")]
    public ActionResult Actualizar(int id, [FromBody] Evento evento)
    {
        try
        {
            bool actualizado = _service.Actualizar(id, evento);

            if (!actualizado)
                return NotFound(new { mensaje = "El evento no existe." });

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    // DELETE: api/eventos/1
    [HttpDelete("{id:int}")]
    public ActionResult Eliminar(int id)
    {
        bool eliminado = _service.Eliminar(id);

        if (!eliminado)
            return NotFound(new { mensaje = "El evento no existe." });

        return NoContent();
    }
}