using Hilfswerk.Models;
using System.Threading.Tasks;

namespace Hilfswerk.Core.Stores
{
    public interface IHelferStore
    {
        Task<Helfer[]> FindHelfer(HelferFilter filter);
        Task<Helfer[]> FindByName(string searchTerm);
        Task<Helfer> GetHelfer(string helferId);
        Task<Einsatz[]> GetEinsaetze(string helferId);
        Task<Einsatz> GetEinsatz(string helferId, string einsatzId);
        Task<int> CountEinsaetze(string helferId);
        Task<Taetigkeit[]> GetTaetigkeiten(string helferId);
        Task<Helfer> AddHelfer(HelferCreateModel createModel);
        Task EditHelfer(string helferId, HelferEditModel editModel);
        Task SetAusgelastet(string helferId, bool istAusgelastet);
        Task<Einsatz> AddEinsatz(string helferId, EinsatzCreateModel createModel);
        Task<Einsatz> EditEinsatz(string helferId, string einsatzId, EinsatzEditModel einsatzEdit);
        Task RemoveEinsatz(string helferId, string einsatzId);
    }
}
