using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GOToPTTK.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MountainGroupsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;


        public MountainGroupsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<string> GetMountainRegionsNames()
        {
            return _dbContext.GrupaGorska.Select(m => m.Nazwa).ToList();
        }
    }
}