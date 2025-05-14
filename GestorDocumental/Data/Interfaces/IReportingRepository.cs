using GestorDocumental.Business.Interfaces;

namespace GestorDocumental.Data.Interfaces
{
    public interface IReportingRepository
    {
        Task<List<TopItemDto>> GetTop10DocumentosAsync();
        Task<List<TopItemDto>> GetTop10CarpetasAsync();
        Task<List<UsageDailyDto>> GetUsageDailyAsync(DateTime fromDate, DateTime toDate);
    }
}
