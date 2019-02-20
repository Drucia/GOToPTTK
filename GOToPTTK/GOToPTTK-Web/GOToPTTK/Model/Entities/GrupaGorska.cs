using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class GrupaGorska
    {
        public GrupaGorska()
        {
            OdcinekPunktowany = new HashSet<OdcinekPunktowany>();
            OdcinekWłasny = new HashSet<OdcinekWłasny>();
            UprawnieniePrzodownika = new HashSet<UprawnieniePrzodownika>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nazwa { get; set; }
        [Required]
        [StringLength(255)]
        public string RegionGorski { get; set; }

        [ForeignKey("RegionGorski")]
        [InverseProperty("GrupaGorska")]
        public RegionGorski RegionGorskiNavigation { get; set; }
        [InverseProperty("GrupaGorska")]
        public ICollection<OdcinekPunktowany> OdcinekPunktowany { get; set; }
        [InverseProperty("GrupaGorska")]
        public ICollection<OdcinekWłasny> OdcinekWłasny { get; set; }
        [InverseProperty("GrupaGorska")]
        public ICollection<UprawnieniePrzodownika> UprawnieniePrzodownika { get; set; }
    }
}
