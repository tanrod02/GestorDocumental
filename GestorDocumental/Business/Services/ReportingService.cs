using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IReportingRepository _reportingRepository;

        public ReportingService(IReportingRepository reportingRpeository)
        {
            _reportingRepository = reportingRpeository;
        }

        public async Task<List<TopItemDto>> GetTop10CarpetasAsync()
        {
            return await _reportingRepository.GetTop10CarpetasAsync();
        }

        public async Task<List<TopItemDto>> GetTop10DocumentosAsync()
        {
            return await _reportingRepository.GetTop10DocumentosAsync();
        }

        public async Task<List<UsageDailyDto>> GetUsageDailyAsync(DateTime fromDate, DateTime toDate)
        {
            return await _reportingRepository.GetUsageDailyAsync(fromDate, toDate);
        }
    }
}
