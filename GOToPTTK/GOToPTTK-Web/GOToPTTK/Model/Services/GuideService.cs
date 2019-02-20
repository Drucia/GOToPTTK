using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GOToPTTK.Model.Entities;

namespace GOToPTTK.Model.Services
{
    public class GuideService : IGuideService
    {
        public Przodownik GetGuide(IList<Przodownik> guides, int id)
        {
            return guides.FirstOrDefault(g => g.UzytkownikId == id);
        }
    }
}
