using GraphQL.Types;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi
{
    public class EinsatzEditInputType : InputObjectGraphType<EinsatzEditModel>
    {
        public EinsatzEditInputType()
        {
            Name = "EinsatzEditInput";
            Field(p => p.Anmerkungen);
            Field(p => p.VermitteltAm);
            Field(p => p.Dauer, nullable: true);
        }
    }
}
