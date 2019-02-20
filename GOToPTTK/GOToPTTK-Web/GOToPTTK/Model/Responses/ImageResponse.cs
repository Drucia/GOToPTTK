using GOToPTTK.Controllers.ApiControllers;
using GOToPTTK.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Responses
{
    public class ImageResponse
    {
        public int Id { get; set; }
        public string Subtitle { get; set; }
        public FileResult Image { get; set; }
        public int TripRouteId { get; set; }

        public static ImageResponse BuildFromModel(Zdjecie image, FileResult res)
        {
            var response = new ImageResponse()
            {
                Id = image.Id,
                Subtitle = image.Podpis,
                Image = res,
                TripRouteId = image.OdcinekId
            };
            return response;
        }
    }
}
