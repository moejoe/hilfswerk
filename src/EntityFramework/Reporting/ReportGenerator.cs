using Hilfswerk.Core.Models;
using Hilfswerk.Core.Reporting;
using Hilfswerk.EntityFramework;
using Hilfswerk.EntityFramework.Entities;
using Hilfswerk.EntityFramework.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Reporting
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly HilfswerkDbContext _hilfswerkDbContext;
        public ReportGenerator(HilfswerkDbContext hilfswerkDbContext)
        {
            _hilfswerkDbContext = hilfswerkDbContext ?? throw new ArgumentNullException(nameof(hilfswerkDbContext));
        }
        public async Task<ReportByMonth> GenerateReport()
        {
            var details = await (from detail in _hilfswerkDbContext.Einsaetze
                                 select new
                                 {
                                     detail.VermitteltAm.Year,
                                     detail.VermitteltAm.Month,
                                     detail.TaetigkeitId,
                                     Einsatz = 1,
                                     HelferId = detail.Helfer.Id,
                                     detail.Dauer
                                 }).ToArrayAsync();



            //select new 
            //{
            //    Year = monthG.Key.Year,
            //    Month = monthG.Key.Month,
            //    Details = (
            //              select new
            //              {
            //                  Taetigkeit = taetigG.Key
            //              }).ToArray()

            //    //(from details in monthG
            //    //           group details by details.Taetigkeit.Id into taetigG
            //    //           select new 
            //    //           {
            //    //               Taetigkeit = taetigG.Key,
            //    //               Einsaetze = taetigG.Count(),
            //    //               Helfer = taetigG.Select(p => p.Helfer.Id).Distinct().Count(),
            //    //               Stunden = taetigG.Sum(p => p.Stunden) ?? 0
            //    //           })
            //}).ToArrayAsync();
            return new ReportByMonth
            {
                HelferInnen = await _hilfswerkDbContext.Helfer.CountAsync(),
                Groups = details.GroupBy(p => new { p.Year, p.Month })
                .Select(p => new ReportGroupMonth
                {
                    Year = p.Key.Year,
                    Month = p.Key.Month,
                    Details = p.GroupBy(q => q.TaetigkeitId)
                    .Select(q => new ReportDetail
                    {
                        Taetigkeit = Projector.CompiledTaetigkeitProjection.Invoke(q.Key),
                        Einsaetze = q.Sum(r => r.Einsatz),
                        Helfer = q.Select(r => r.HelferId).Distinct().Count(),
                        Dauer = q.Aggregate(TimeSpan.Zero, (acc, cur) => acc += (cur?.Dauer ?? TimeSpan.Zero))
                    }).ToArray()
                }).ToArray()
            };
        }
    }
}
