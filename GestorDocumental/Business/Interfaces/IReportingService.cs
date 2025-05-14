namespace GestorDocumental.Business.Interfaces
{
    public interface IReportingService
    {
        Task<List<TopItemDto>> GetTop10DocumentosAsync();
        Task<List<TopItemDto>> GetTop10CarpetasAsync();
        Task<List<UsageDailyDto>> GetUsageDailyAsync(DateTime fromDate, DateTime toDate);
    }

    public class TopItemDto
    {
        public int TargetId { get; set; }
        public string Nombre { get; set; }
        public int NumEventos { get; set; }
        public double TotalPeso { get; set; }
    }

    public class UsageDailyDto
    {
        public int TargetType { get; set; }
        public int TargetId { get; set; }
        public DateTime UsageDate { get; set; }
        public int NumEventos { get; set; }
        public double TotalPeso { get; set; }
    }
}
