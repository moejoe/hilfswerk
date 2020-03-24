using GraphQL.Types;
using Hilfswerk.EntityFramework.Entities;

namespace Hilfswerk.GraphApi
{
    public class EinsatzType : ObjectGraphType<Einsatz>
    {
        public EinsatzType()
        {
            Name = "Einsatz";
            Field<HelferType>("helfer", resolve: x => x.Source.Helfer);
            Field(p => p.Hilfesuchender);
            Field<TaetigkeitEnumType>("taetigkeit", resolve: p => (Core.Models.Taetigkeit)p.Source.TaetigkeitId);
            Field(p => p.VermitteltDurch);
            Field(p => p.VermitteltAm);
            Field(p => p.Anmerkungen);
        }
    }
}
