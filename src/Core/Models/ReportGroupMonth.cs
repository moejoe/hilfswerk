namespace Hilfswerk.Core.Models
{
    public class ReportGroupMonth
    {
        public ReportDetail[] Details { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int HelferInnen { get; set; }
    }
}
