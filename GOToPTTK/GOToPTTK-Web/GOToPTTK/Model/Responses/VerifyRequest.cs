using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class VerifyRequest
    {
        public DateTime VerifyDate { get; set; }
        public DateTime SendVerifyDate { get; set; }
        public string Issues { get; set; }
        public int TripId { get; set; }
        public int GuideId { get; set; }
        public int? TripRouteId { get; set; }
        public string VerificationStatusId { get; set; }

        public static VerifyRequest BuildFromModel(Weryfikacja verify)
        {
            VerifyRequest request = new VerifyRequest()
            {
                VerifyDate = verify.DataWeryfikacji,
                SendVerifyDate = verify.DataWyslaniaDoWeryfikacji,
                Issues = verify.Uwagi,
                TripId = verify.WycieczkaId,
                GuideId = verify.PrzodownikUzytkownikId,
                TripRouteId = verify.OdcinekId == null ? -1 : verify.OdcinekId,
                VerificationStatusId = verify.StatusWeryfikacji
            };

            return request;
        }
    }
}
