﻿using GraphQL.Types;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi
{
    public class EinsatzInputType : InputObjectGraphType<EinsatzCreateModel>
    {
        public EinsatzInputType()
        {
            Name = "EinsatzInput";
            Field(p => p.Hilfesuchender);
            Field<TaetigkeitEnumType>("taetigkeit", resolve: p => p.Source.Taetigkeit);
            Field(p => p.VermitteltDurch);
            Field(p => p.Anmerkungen);
        }
    }
}
