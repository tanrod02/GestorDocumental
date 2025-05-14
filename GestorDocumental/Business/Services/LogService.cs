using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Enums;
using GestorDocumental.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace GestorDocumental.Business.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task LogAsync(int userId, bool isDocumento, int targetId, TipoAccion action)
        {
            await _logRepository.LogAsync(userId, isDocumento, targetId, action);
        }
    }
}
