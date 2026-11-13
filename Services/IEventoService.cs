using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;
    }

    public List<Evento> ObtenerTodas()
    {
        return _repository.ObtenerTodas();
    }

    public Evento? ObtenerPorId(int id)
    {
        if (id <= 0)
            return null;

        return _repository.ObtenerPorId(id);
    }

    public Evento Agregar(Evento evento)
    {
        ValidarEvento(evento);

        evento.Id = 0;

        return _repository.Agregar(evento);
    }

    public bool Actualizar(int id, Evento evento)
    {
        if (id <= 0)
            return false;

        ValidarEvento(evento);

        var existente = _repository.ObtenerPorId(id);

        if (existente == null)
            return false;

        evento.Id = id;

        return _repository.Actualizar(id, evento);
    }

    public bool Eliminar(int id)
    {
        if (id <= 0)
            return false;

        return _repository.Eliminar(id);
    }

    // Validar que el nombre sea obligatorio, 
    // el cupo sea mayor que cero y la fecha sea válida.

    private static void ValidarEvento(Evento evento)
    {
        if (evento == null)
            throw new ArgumentNullException(nameof(evento));

        if (string.IsNullOrWhiteSpace(evento.Nombre))
            throw new ArgumentException("El nombre de la misión es obligatorio.");

        if (evento.CupoMaximo <= 0)
            throw new ArgumentException("El cupo máximo debe ser mayor que cero.");

        if (evento.Fecha < DateTime.Today || evento.Fecha > DateTime.Today.AddYears(1))
            throw new ArgumentException("La fecha del evento debe ser válida.");
    }
}