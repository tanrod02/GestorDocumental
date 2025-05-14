namespace GestorDocumental.Data.Entities
{
    public class LogSettings
    {
        public double DefaultWeight { get; set; }
        public Dictionary<string, double> Weights { get; set; }
        public double CarpetaMultiplier { get; set; }
        public double DocumentoMultiplier { get; set; }
        public Dictionary<int, double> RoleMultipliers { get; set; }
    }
}
