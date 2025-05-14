using GestorDocumental.Data.Enums;

namespace GestorDocumental.Data.Entities
{
    public class UserLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public LogTargetType TargetType { get; set; }
        public int TargetId { get; set; }

        public int Count { get; set; }

        public double TotalWeight { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
