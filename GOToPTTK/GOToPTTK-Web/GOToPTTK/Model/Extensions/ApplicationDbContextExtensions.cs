using GOToPTTK.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GOToPTTK.Model.Extensions
{
    public static class ApplicationDbContextExtensions
    {
        public static IList<OdcinekPunktowany> GradedRoutesWithIncludedRelatedData(this ApplicationDbContext context)
        {
            return context.OdcinekPunktowany.Include(r => r.Poczatek).Include(r => r.Koniec).Include(r => r.GrupaGorska).ToList();
        }

        public static IList<OdcinekPunktowany> GetEffectiveRouteList(this ApplicationDbContext context)
        {
            return context.WykazTras
                .Include(rl => rl.OdcinekPunktowany)
                    .ThenInclude(r => r.Poczatek)
                .Include(rl => rl.OdcinekPunktowany)
                    .ThenInclude(r => r.Koniec)
                .Include(rl => rl.OdcinekPunktowany)
                    .ThenInclude(r => r.GrupaGorska)
                .FirstOrDefault(rl => rl.Obowiazuje)
                .OdcinekPunktowany
                .ToList();
        }

        public static IList<Wycieczka> TripsWithIncludedRelatedData(this ApplicationDbContext context)
        {
            return context.Wycieczka
                .Include(t => t.PrzodownikUzytkownik)
                .ThenInclude(u => u.Uzytkownik)
                .Include(t => t.TurystaUzytkownik)
                .ThenInclude(v => v.Uzytkownik)
                .Include(t => t.Odcinek)
                .ThenInclude(ro => ro.OdcinekPunktowany)
                .ThenInclude(p => p.Poczatek)
                .Include(t => t.Odcinek)
                .ThenInclude(ro => ro.OdcinekPunktowany)
                .ThenInclude(p => p.Koniec)
                .Include(t => t.Odcinek)
                .ThenInclude(ro => ro.OdcinekWłasny)
                .ThenInclude(p => p.Poczatek)
                .Include(t => t.Odcinek)
                .ThenInclude(ro => ro.OdcinekWłasny)
                .ThenInclude(p => p.Koniec).ToList();
        }

        public static IList<Wycieczka> GradedTripsWithIncludedObjs(this ApplicationDbContext context)
        {

            return context.Wycieczka
                .Include(t => t.PrzodownikUzytkownik)
                    .ThenInclude(u => u.Uzytkownik)
                .Include(t => t.TurystaUzytkownik)
                    .ThenInclude(v => v.Uzytkownik)
                .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.OdcinekPunktowany)
                    .ThenInclude(p => p.Poczatek)
                .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.OdcinekPunktowany)
                    .ThenInclude(p => p.Koniec)
                .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.OdcinekPunktowany)
                    .ThenInclude(p => p.GrupaGorska)
                .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.OdcinekWłasny)
                    .ThenInclude(p => p.Poczatek)
                .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.OdcinekWłasny)
                    .ThenInclude(p => p.Koniec)
               .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.OdcinekWłasny)
                    .ThenInclude(p => p.GrupaGorska)
                .Include(t => t.Odcinek)
                    .ThenInclude(ro => ro.ZweryfikowanyNavigation)
                .ToList();
        }

        public static IList<Przodownik> GradedGuidesWithIncludedUsers(this ApplicationDbContext context)
        {
            return context.Przodownik.Include(g => g.UprawnieniePrzodownika).ToList();
        }
        public static IList<Odcinek> GradedTripRoutesWithIncludedObjects(this ApplicationDbContext context)
        {
            return context.Odcinek
                .Include(ro => ro.OdcinekPunktowany)
                    .ThenInclude(p => p.Poczatek)
                .Include(ro => ro.OdcinekPunktowany)
                    .ThenInclude(p => p.Koniec)
                .Include(ro => ro.OdcinekPunktowany)
                    .ThenInclude(p => p.GrupaGorska)
                .Include(ro => ro.OdcinekWłasny)
                    .ThenInclude(p => p.Poczatek)
                .Include(ro => ro.OdcinekWłasny)
                    .ThenInclude(p => p.Koniec)
                .Include(ro => ro.OdcinekWłasny)
                    .ThenInclude(p => p.GrupaGorska)
                .Include(r => r.ZweryfikowanyNavigation).ToList();
        }

        public static IList<Odcinek> GradedTripRoutesWithIncludedRoutes(this ApplicationDbContext context)
        {
            return context.Odcinek
                .Include(ro => ro.OdcinekPunktowany)
                .Include(ro => ro.OdcinekWłasny).ToList();
        }
    }
};