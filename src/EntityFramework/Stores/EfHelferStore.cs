using Hilfswerk.Core.Stores;
using Hilfswerk.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hilfswerk.EntityFramework.Stores
{

    public class EfHelferStore : IHelferStore
    {
        private readonly HilfswerkDbContext _db;

        public EfHelferStore(HilfswerkDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<Einsatz[]> GetEinsaetze(string helferId)
        {
            return await _db.Helfer.Where(p => p.Id == helferId)
                .SelectMany(p => p.Einsaetze)
                .Select(Projector.EinsatzProjection)
                .ToArrayAsync();
        }
        public async Task<int> CountEinsaetze(string helferId)
        {
            return await _db.Einsaetze
                .Where(p => p.Helfer.Id == helferId)
                .CountAsync();
        }

        public async Task<Helfer> GetHelfer(string helferId)
        {
            return await _db.Helfer
                .Select(Projector.HelferProjection)
                .SingleOrDefaultAsync(p => p.Id == helferId);
        }

        public async Task<Helfer[]> FindHelfer(HelferFilter filter)
        {
            var helferQuery = from helfer in _db.Helfer
                                .Include(p => p.HelferTaetigkeiten)
                              select helfer;
            if (filter.PlzFilter.Any())
            {
                helferQuery = helferQuery.Where(p => filter.PlzFilter.Contains(p.Kontakt.Plz));
            }

            if (filter.TaetigkeitFilter.Any())
            {
                var taetigkeiten = filter.TaetigkeitFilter.Cast<int>().ToArray();
                helferQuery = helferQuery.Where(p => p.HelferTaetigkeiten.Any(q => taetigkeiten.Contains(q.TaetigkeitId)));
            }

            if (filter.IstRisikoGruppeFilter.HasValue)
            {
                helferQuery = helferQuery.Where(p => p.istRisikogrupepe == filter.IstRisikoGruppeFilter.Value);
            }

            if (filter.HatAutoFilter.HasValue)
            {
                helferQuery = helferQuery.Where(p => p.hatAuto == filter.HatAutoFilter.Value);
            }

            if (filter.IstFreiwilligerFilter.HasValue)
            {
                helferQuery = helferQuery.Where(p => p.istFreiwilliger == filter.IstFreiwilligerFilter.Value);
            }

            if (filter.IstZivildienerFilter.HasValue)
            {
                helferQuery = helferQuery.Where(p => p.istZivildiener == filter.IstZivildienerFilter.Value);
            }
            if (filter.IstAusgelastetFilter.HasValue)
            {
                helferQuery = helferQuery.Where(p => p.istAusgelastet == filter.IstAusgelastetFilter.Value);
            }

            return await helferQuery
                .Select(Projector.HelferProjection)
                .ToArrayAsync();
        }

        public async Task<Taetigkeit[]> GetTaetigkeiten(string helferId)
        {
            return await _db.Helfer.Where(p => p.Id == helferId)
                .SelectMany(p => p.HelferTaetigkeiten)
                .Select(p => Projector.TaetigkeitFromId(p.TaetigkeitId))
                .ToArrayAsync();
        }

        public async Task<Helfer> AddHelfer(HelferCreateModel createModel)
        {
            var helfer = createModel.ToEntity();
            helfer.Id = Guid.NewGuid().ToString(); ;
            _db.Add(helfer);
            await _db.SaveChangesAsync();
            return Projector.HelferProjection.Compile().Invoke(helfer);
        }

        public async Task<Einsatz> AddEinsatz(string helferId, EinsatzCreateModel createModel)
        {
            var helfer = await _db.Helfer.SingleOrDefaultAsync(p => p.Id == helferId) ?? throw new InvalidOperationException($"Helfer {helferId} not found");
            var einsatz = new Entities.Einsatz
            {
                Helfer = helfer,
                Id = Guid.NewGuid().ToString(),
                VermitteltAm = DateTimeOffset.Now.UtcDateTime
            };
            createModel.ApplyTo(einsatz);
            _db.Add(einsatz);
            await _db.SaveChangesAsync();
            return Projector.EinsatzProjection.Compile().Invoke(einsatz);
        }

        public Task<Helfer[]> FindByName(string searchTerm)
        {
            var terms = Regex.Split(searchTerm, @"\s+");
            var query = _db.Helfer.AsQueryable();
            foreach (var term in terms)
            {
                query = query.Where(p => p.Kontakt.Nachname.ToUpper().Contains(term.ToUpper()) || p.Kontakt.Vorname.ToUpper().Contains(term.ToUpper()));
            }
            return query.Select(Projector.HelferProjection).ToArrayAsync();
        }
    }
}
