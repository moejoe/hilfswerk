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
        Task<int> CountEinsaetze(string helferId);
        Task<Taetigkeit[]> GetTaetigkeiten(string helferId);
        Task<Helfer> AddHelfer(HelferCreateModel createModel);
        Task<Einsatz> AddEinsatz(string helferId, EinsatzCreateModel createModel);
    }
}
