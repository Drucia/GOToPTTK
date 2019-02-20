using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Services
{
    public class VerifyService : IVerifyService
    {
        public bool CanGuideVerifyTrip(IList<Wycieczka> trips, int gId, int tId)
        {
            return trips?.Any(t => t.Id == tId && t.PrzodownikUzytkownikId == gId) ?? false;
        }

        public void CheckForUpdateTripStatus(int tId, String stateId, ApplicationDbContext _context, bool isTripVerify)
        {
            var tr = _context.Wycieczka.FirstOrDefault(t => t.Id == tId);
            var routes = _context.Odcinek.Where(r => r.WycieczkaId == tId);

            if (stateId.Equals("ZATWIERDZONA"))
            {
                if (isTripVerify)
                { // think about new verify with this
                    foreach (Odcinek r in routes)
                    {
                        r.Zweryfikowany = stateId;
                        _context.Odcinek.Update(r);
                    }
                    tr.Zweryfikowana = "ZATWIERDZONA";
                    _context.Update(tr);
                }
                else // IS ROUTE VERIFY
                {
                    bool is_all_apply = routes.All(r => r.Zweryfikowany.Equals(stateId));

                    if (is_all_apply)
                    {
                        tr.Zweryfikowana = stateId;
                        _context.Wycieczka.Update(tr);
                    } // ALL ARE ZATWIERDZONE
                }
            }
            else
            {
                tr.Zweryfikowana = "ODRZUCONA";
                _context.Wycieczka.Update(tr);
            } // is ODRZUCONA

            _context.SaveChanges();
        }

        public bool ExistInVerificationsList(IList<Weryfikacja> verifications, Weryfikacja verify)
        {
            return verifications.Any(v => v.isEqual(verify));
        }
    }
}
