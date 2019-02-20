using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class PlaceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public float? Altitude { get; set; }

        public Miejsce ToModel()
        {
            Miejsce place = new Miejsce()
            {
                Nazwa = this.Name,
                DlugoscGeograficzna = Longitude,
                SzerokoscGeograficzna = Latitude,
                WysokoscNpm = Altitude ?? 0
            };
            if (Id > 0)
            {
                place.Id = this.Id;
            }
            return place;

        }

        public static PlaceResponse BuildFromModel(Miejsce place)
        {
            var response = new PlaceResponse()
            {
                Id = place.Id,
                Name = place.Nazwa,
                Longitude = place.DlugoscGeograficzna,
                Latitude = place.SzerokoscGeograficzna,
                Altitude = place.WysokoscNpm
            };

            return response;
        }
    }
}
