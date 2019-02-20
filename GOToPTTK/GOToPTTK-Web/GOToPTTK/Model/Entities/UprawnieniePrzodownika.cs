using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class UprawnieniePrzodownika
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("PrzodownikUzytkownikID")]
        public int PrzodownikUzytkownikId { get; set; }
        [Column("GrupaGorskaID")]
        public int GrupaGorskaId { get; set; }

        [ForeignKey("GrupaGorskaId")]
        [InverseProperty("UprawnieniePrzodownika")]
        public GrupaGorska GrupaGorska { get; set; }
        [ForeignKey("PrzodownikUzytkownikId")]
        [InverseProperty("UprawnieniePrzodownika")]
        public Przodownik PrzodownikUzytkownik { get; set; }
    }
}
