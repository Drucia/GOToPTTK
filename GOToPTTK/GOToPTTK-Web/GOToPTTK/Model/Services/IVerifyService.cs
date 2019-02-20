using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Services
{
    public interface IVerifyService
    {
        void CheckForUpdateTripStatus(int tId, String stateId, ApplicationDbContext _context, bool isTripVerify);
        bool CanGuideVerifyTrip(IList<Wycieczka> trips, int gId, int tId);
        bool ExistInVerificationsList(IList<Weryfikacja> verifications, Weryfikacja verify);
    }
}
