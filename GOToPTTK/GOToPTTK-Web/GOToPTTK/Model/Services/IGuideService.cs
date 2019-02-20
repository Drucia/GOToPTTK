using GOToPTTK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Services
{
    public interface IGuideService
    {
        Przodownik GetGuide(IList<Przodownik> guides, int id);
    }
}
