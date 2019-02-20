
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Odcinek
    {
        public Odcinek()
        {
            Weryfikacja = new HashSet<Weryfikacja>();
            Zdjecie = new HashSet<Zdjecie>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column("WycieczkaID")]
        public int WycieczkaId { get; set; }
        [Column("OdcinekPunktowanyID")]
        public int? OdcinekPunktowanyId { get; set; }
        [Column("OdcinekWłasnyID")]
        public int? OdcinekWłasnyId { get; set; }
        [StringLength(20)]
        public string Zweryfikowany { get; set; }

        [ForeignKey("OdcinekPunktowanyId")]
        [InverseProperty("Odcinek")]
        public OdcinekPunktowany OdcinekPunktowany { get; set; }
        [ForeignKey("OdcinekWłasnyId")]
        [InverseProperty("Odcinek")]
        public OdcinekWłasny OdcinekWłasny { get; set; }
        [ForeignKey("WycieczkaId")]
        [InverseProperty("Odcinek")]
        public Wycieczka Wycieczka { get; set; }
        [ForeignKey("Zweryfikowany")]
        [InverseProperty("Odcinek")]
        public StatusWeryfikacji ZweryfikowanyNavigation { get; set; }
        [InverseProperty("Odcinek")]
        public ICollection<Weryfikacja> Weryfikacja { get; set; }
        [InverseProperty("Odcinek")]
        public ICollection<Zdjecie> Zdjecie { get; set; }
    }
}
