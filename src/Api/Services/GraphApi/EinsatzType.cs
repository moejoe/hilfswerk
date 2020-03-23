using GraphQL.Types;
using Hilfswerk.Core.Models;

namespace Hilfswerk.GraphApi
{
    public class EinsatzType : ObjectGraphType<Einsatz>
    {
        public EinsatzType()
        {
            Name = "Einsatz";
            //Field<HelferType>("helfer", resolve: x => x.Source.Helfer);
            Field(p => p.Hilfesuchender);
            Field<TaetigkeitEnumType>("taetigkeit", resolve: p => p.Source.Taetigkeit);
            Field(p => p.VermitteltDurch);
            Field(p => p.VermitteltAm);
            Field(p => p.Anmerkungen);
        }
    }
}
