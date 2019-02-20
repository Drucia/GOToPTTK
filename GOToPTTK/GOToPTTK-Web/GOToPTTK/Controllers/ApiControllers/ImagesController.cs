using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using GOToPTTK.Model.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GOToPTTK.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/images/2/start
        [HttpGet("{routeid}/start")]
        public FileResult GetImageStart([FromRoute] int routeid)
        {
            var image = _context.Zdjecie.FirstOrDefault(i => i.OdcinekId == routeid && i.Podpis.Equals("start"));

            if (image != null)
                return File(image.Obraz, "image/jpeg");
            return null;
        }

        // GET: api/images/2/end
        [HttpGet("{routeid}/end")]
        public FileResult GetImageEnd([FromRoute] int routeid)
        {
            var image = _context.Zdjecie.FirstOrDefault(i => i.OdcinekId == routeid && i.Podpis.Equals("end"));
            if (image != null)
                return File(image.Obraz, "image/jpeg");
            return null;
        }
    }
}