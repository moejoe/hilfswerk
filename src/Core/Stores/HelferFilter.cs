using Hilfswerk.Models;

namespace Hilfswerk.Core.Stores
{
    public class HelferFilter
    {
        public int[] PlzFilter { get; set; } = System.Array.Empty<int>();
        public Taetigkeit[] TaetigkeitFilter { get; set; } = System.Array.Empty<Taetigkeit>();
        public bool? IstRisikoGruppeFilter { get; set; }
        public bool? HatAutoFilter { get; set; }
    }
}