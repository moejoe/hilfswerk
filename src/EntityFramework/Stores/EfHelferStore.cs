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
                .Include(v => v.Einsaetze)
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
                Id = Guid.NewGuid().ToString()
            };
            createModel.ApplyTo(einsatz);
            _db.Add(einsatz);
            helfer.istAusgelastet = createModel.HelferAusgelastet;
            await _db.SaveChangesAsync();
            return Projector.EinsatzProjection.Compile().Invoke(einsatz);
        }

        public Task<Helfer[]> FindByName(string searchTerm)
        {
            var terms = Regex.Split(searchTerm, @"\s+");
            var query = _db.Helfer.AsQueryable();
            foreach (var term in terms)
            {
                query = query.Where(p => HilfswerkDbContext.ContainsIgnoreCase(term, p.Kontakt.Nachname) || HilfswerkDbContext.ContainsIgnoreCase(term, p.Kontakt.Vorname));
            }
            return query.Select(Projector.HelferProjection).ToArrayAsync();
        }

        public async Task EditHelfer(string helferId, HelferEditModel editModel)
        {
            var helfer = await _db.Helfer
                .Include(h => h.Kontakt)
                .Include(h => h.HelferTaetigkeiten)
                .SingleOrDefaultAsync(p => p.Id == helferId) ?? throw new InvalidOperationException($"Helfer {helferId} not found");
            editModel.ApplyTo(helfer);
            var newTaetigkeiten = editModel.Taetigkeiten.Select(p => new Entities.HelferTaetigkeit { TaetigkeitId = (int)p, HelferId = helfer.Id }).ToArray();
            foreach (var existingTaetigkeit in helfer.HelferTaetigkeiten)
            {
                if (newTaetigkeiten.All(p => p.TaetigkeitId != existingTaetigkeit.TaetigkeitId))
                {
                    _db.Remove(existingTaetigkeit);
                }
            }
            foreach (var newTaetigkeit in newTaetigkeiten)
            {
                if (helfer.HelferTaetigkeiten.All(p => p.TaetigkeitId != newTaetigkeit.TaetigkeitId))
                {
                    _db.Add(newTaetigkeit);
                }
            }
            await _db.SaveChangesAsync();
        }

        public async Task SetAusgelastet(string helferId, bool istAusgelastet)
        {
            var helfer = await _db.Helfer
                   .Include(h => h.Kontakt)
                   .SingleOrDefaultAsync(p => p.Id == helferId) ?? throw new InvalidOperationException($"Helfer {helferId} not found");
            helfer.istAusgelastet = istAusgelastet;
            await _db.SaveChangesAsync();
        }

        public Task<Einsatz> GetEinsatz(string helferId, string einsatzId)
        {
            return _db.Einsaetze.Where(p => p.Id == einsatzId && p.Helfer.Id == helferId)
                    .Select(Projector.EinsatzProjection)
                    .SingleOrDefaultAsync();
        }

        public async Task<Einsatz> EditEinsatz(string helferId, string einsatzId, EinsatzEditModel einsatzEdit)
        {
            var einsatz = (await _db.Einsaetze.Where(p => p.Id == einsatzId && p.Helfer.Id == helferId)
                .Include(p => p.Helfer)
                .SingleOrDefaultAsync()) ?? throw new InvalidOperationException($"Einsatz {einsatzId} for helfer {helferId} not found");

            einsatzEdit.ApplyTo(einsatz);
            await _db.SaveChangesAsync();
            return Projector.EinsatzProjection.Compile().Invoke(einsatz);   
        }
    }
}
