namespace Hilfswerk.Core.Models
{
    public class ReportByMonth
    {
        public int HelferInnenGesamt { get; set; }
        public int HelferInnenEingesetzt { get; set; }
        public ReportGroupMonth[] Groups { get; set; }
    }
}
