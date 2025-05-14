using GestorDocumental.Data.Entities;
using System.Runtime;
using GestorDocumental.Data.Enums;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GestorDocumental.Data.Repositories
{
    public class LogRepository : ILogRepository
    {

        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;
        private readonly LogSettings _settings;


        public LogRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory, IOptions<LogSettings> opts)
        {
            _contextFactory = contextFactory;
            _settings = opts.Value;
        }


        public async Task LogAsync(int userId, bool isDocumento, int targetId, TipoAccion action)
        {
            using var context = _contextFactory.CreateDbContext();

            var targetType = isDocumento ? LogTargetType.Documento : LogTargetType.Carpeta;

            // 1) Calcular peso base
            double weight = _settings.DefaultWeight;
            if (_settings.Weights.TryGetValue(action.ToString(), out var w))
                weight = w;

            // 2) Aplicar multiplicador por tipo
            weight *= isDocumento
                ? _settings.DocumentoMultiplier
                : _settings.CarpetaMultiplier;

            // 3) Aplicar multiplicador por rol de usuario
            var roleFactor = _settings.RoleMultipliers
                .GetValueOrDefault(userId, 1.0);
            weight *= roleFactor;

            // 4) Grabar auditoría individual
            var audit = new UserLogAudit
            {
                UserId = userId,
                TargetType = targetType,
                TargetId = targetId,
                ActionType = action,
                Weight = weight,
                OccurredAt = DateTime.UtcNow
            };
            context.UserLogAudits.Add(audit);

            // 5) Actualizar o crear registro acumulado
            var log = await context.UserLogs
                .FirstOrDefaultAsync(l =>
                    l.UserId == userId &&
                    l.TargetType == targetType &&
                    l.TargetId == targetId);

            if (log == null)
            {
                log = new UserLog
                {
                    UserId = userId,
                    TargetType = targetType,
                    TargetId = targetId,
                    Count = 1,
                    TotalWeight = weight,
                    LastUpdated = DateTime.UtcNow
                };
                context.UserLogs.Add(log);
            }
            else
            {
                log.Count++;
                log.TotalWeight += weight;
                log.LastUpdated = DateTime.UtcNow;
                context.UserLogs.Update(log);
            }

            await context.SaveChangesAsync();
        }
    }
}
