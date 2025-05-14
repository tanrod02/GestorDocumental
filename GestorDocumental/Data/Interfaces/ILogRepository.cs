using GestorDocumental.Data.Enums;

namespace GestorDocumental.Data.Interfaces
{
    public interface ILogRepository
    {
        Task LogAsync(int userId, bool isDocumento, int targetId, TipoAccion action);
    }
}
