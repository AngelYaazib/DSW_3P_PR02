using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Repositories;

public interface IEventoRepository
{
    List<Evento> ObtenerTodas();
    Evento? ObtenerPorId(int id);
    Evento Agregar(Evento nuevaMision);
    bool Actualizar(int id, Evento misionActualizada);
    bool Eliminar(int id); 
}