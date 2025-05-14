using GestorDocumental.Data.Enums;

namespace GestorDocumental.Data.Entities
{
    public class UserLogAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public LogTargetType TargetType { get; set; }
        public int TargetId { get; set; }
        public TipoAccion ActionType { get; set; }
        public double Weight { get; set; }

        public DateTime OccurredAt { get; set; }
    }
}
