using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Enums;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class ReportingRepository : IReportingRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;

        public ReportingRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<TopItemDto>> GetTop10CarpetasAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context
            .Set<UserLogAudit>()                             // UserLogAudits
            .Where(a => a.TargetType == LogTargetType.Documento)
            .GroupBy(a => a.TargetId)
            .Select(g => new TopItemDto
            {
                TargetId = g.Key,
                NumEventos = g.Count(),
                TotalPeso = g.Sum(x => x.Weight),
                Nombre = context.Archivos
                                  .Where(x => x.CodigoArchivo == g.Key)
                                  .Select(x => x.NombreArchivo)
                                  .FirstOrDefault() ?? ""
            })
            .OrderByDescending(x => x.TotalPeso)
            .Take(10)
            .ToListAsync();
        }

        public async Task<List<TopItemDto>> GetTop10DocumentosAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await context
            .Set<UserLogAudit>()
            .Where(a => a.TargetType == LogTargetType.Carpeta)
            .GroupBy(a => a.TargetId)
            .Select(g => new TopItemDto
            {
                TargetId = g.Key,
                NumEventos = g.Count(),
                TotalPeso = g.Sum(x => x.Weight),
                Nombre = context.Carpeta
                                  .Where(c => c.CodigoCarpeta == g.Key)
                                  .Select(c => c.Descripcion)
                                  .FirstOrDefault() ?? ""
            })
            .OrderByDescending(x => x.TotalPeso)
            .Take(10)
            .ToListAsync();

        }

        public async Task<List<UsageDailyDto>> GetUsageDailyAsync(DateTime fromDate, DateTime toDate)
        {
            using var context = _contextFactory.CreateDbContext();

            return await context
            .Set<UserLogAudit>()
            .Where(a => a.OccurredAt.Date >= fromDate.Date
                     && a.OccurredAt.Date <= toDate.Date)
            .GroupBy(a => new { a.TargetType, a.TargetId, Fecha = a.OccurredAt.Date })
            .Select(g => new UsageDailyDto
            {
                TargetType = Convert.ToInt32(g.Key.TargetType),
                TargetId = g.Key.TargetId,
                UsageDate = g.Key.Fecha,
                NumEventos = g.Count(),
                TotalPeso = g.Sum(x => x.Weight)
            })
            .OrderBy(x => x.UsageDate)
            .ThenBy(x => x.TargetType)
            .ThenBy(x => x.TargetId)
            .ToListAsync();
        }
    }
}
