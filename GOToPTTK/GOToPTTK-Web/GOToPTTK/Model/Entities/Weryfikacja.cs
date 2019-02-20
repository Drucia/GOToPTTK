using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Weryfikacja
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataWeryfikacji { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataWyslaniaDoWeryfikacji { get; set; }
        [StringLength(255)]
        public string Uwagi { get; set; }
        [Column("WycieczkaID")]
        public int WycieczkaId { get; set; }
        [Column("PrzodownikUzytkownikID")]
        public int PrzodownikUzytkownikId { get; set; }
        [Required]
        [StringLength(20)]
        public string StatusWeryfikacji { get; set; }
        [Column("OdcinekID")]
        public int? OdcinekId { get; set; }

        [ForeignKey("OdcinekId")]
        [InverseProperty("Weryfikacja")]
        public Odcinek Odcinek { get; set; }
        [ForeignKey("PrzodownikUzytkownikId")]
        [InverseProperty("Weryfikacja")]
        public Przodownik PrzodownikUzytkownik { get; set; }
        [ForeignKey("StatusWeryfikacji")]
        [InverseProperty("Weryfikacja")]
        public StatusWeryfikacji StatusWeryfikacjiNavigation { get; set; }
        [ForeignKey("WycieczkaId")]
        [InverseProperty("Weryfikacja")]
        public Wycieczka Wycieczka { get; set; }
        public bool isEqual(Weryfikacja other)
        {
            return Id != other.Id && (WycieczkaId == other.WycieczkaId && PrzodownikUzytkownikId == other.PrzodownikUzytkownikId && OdcinekId == other.OdcinekId);
        }
    }
}
