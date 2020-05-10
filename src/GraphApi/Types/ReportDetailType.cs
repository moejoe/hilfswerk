using GraphQL.Types;
using Hilfswerk.Core.Models;
using Hilfswerk.Models;
namespace Hilfswerk.GraphApi
{

    public class ReportDetailType : ObjectGraphType<ReportDetail>
    {
        public ReportDetailType()
        {
            Name = "ReportDetail";
            Field<TaetigkeitEnumType>("taetigkeit", resolve: p => p.Source.Taetigkeit);
            Field(p => p.Dauer);
            Field(p => p.HelferInnen);
            Field(p => p.Einsaetze);
        }
    }
}
