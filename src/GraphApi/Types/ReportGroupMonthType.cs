using GraphQL.Types;
using Hilfswerk.Core.Models;

namespace Hilfswerk.GraphApi
{
    public class ReportGroupMonthType : ObjectGraphType<ReportGroupMonth>
    {
        public ReportGroupMonthType()
        {
            Name = "ReportGroupMonth";
            Field(p => p.Year);
            Field(p => p.Month);
            Field<ListGraphType<ReportDetailType>>("details", resolve: p => p.Source.Details);
        }
    }
}
