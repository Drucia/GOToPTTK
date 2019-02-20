using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class RegionGorski
    {
        public RegionGorski()
        {
            GrupaGorska = new HashSet<GrupaGorska>();
        }

        [Key]
        [StringLength(255)]
        public string Nazwa { get; set; }

        [InverseProperty("RegionGorskiNavigation")]
        public ICollection<GrupaGorska> GrupaGorska { get; set; }
    }
}
