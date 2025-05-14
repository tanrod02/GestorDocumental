using GestorDocumental.Data.Enums;

namespace GestorDocumental.Business.Interfaces
{
    public interface ILogService
    {
        Task LogAsync(int userId, bool isDocumento, int targetId, TipoAccion action);
    }
}
