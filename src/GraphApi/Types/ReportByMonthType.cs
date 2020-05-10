using GraphQL.Types;
using Hilfswerk.Core.Models;

namespace Hilfswerk.GraphApi
{
    public class ReportByMonthType : ObjectGraphType<ReportByMonth>
    {
        public ReportByMonthType()
        {
            Name = "ReportByMonth";
            Field<ListGraphType<ReportGroupMonthType>>("groups", resolve: p => p.Source.Groups);
            Field(p => p.HelferInnen);
        }
    }
}
