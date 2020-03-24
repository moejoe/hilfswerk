using GraphQL.Types;
using Hilfswerk.Models;
using Hilfswerk.Core.Stores;

namespace Hilfswerk.GraphApi
{
    public class EinsatzType : ObjectGraphType<Einsatz>
    {
        public EinsatzType(IHelferStore store)
        {
            Name = "Einsatz";
            Field<HelferType>("helfer", resolve: x => store.GetHelfer(x.Source.Helfer.Id));
            Field(p => p.Hilfesuchender);
            Field<TaetigkeitEnumType>("taetigkeit", resolve: p => p.Source.Taetigkeit);
            Field(p => p.VermitteltDurch);
            Field(p => p.VermitteltAm);
            Field(p => p.Anmerkungen);
        }
    }
}
