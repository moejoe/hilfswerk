using Hilfswerk.Core.Models;
using System.Threading.Tasks;

namespace Hilfswerk.Core.Reporting
{
    public interface IReportGenerator
    {
        Task<ReportByMonth> GenerateReport();
    }
}
