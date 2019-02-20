using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Wycieczka
    {
        public Wycieczka()
        {
            Odcinek = new HashSet<Odcinek>();
            Weryfikacja = new HashSet<Weryfikacja>();
        }

        [Column("ID")]
        public int Id { get; set; }
        public int SumaPunktow { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataRozpoczecia { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataZakonczenia { get; set; }
        [Column("GOTID")]
        public int? Gotid { get; set; }
        [Column("TurystaUzytkownikID")]
        public int TurystaUzytkownikId { get; set; }
        [Column("PrzodownikUzytkownikID")]
        public int? PrzodownikUzytkownikId { get; set; }

       
        [StringLength(20)]
        public string Zweryfikowana { get; set; }

        [ForeignKey("Gotid")]
        [InverseProperty("Wycieczka")]
        public Got Got { get; set; }
        [ForeignKey("PrzodownikUzytkownikId")]
        [InverseProperty("Wycieczka")]
        public Przodownik PrzodownikUzytkownik { get; set; }
        [ForeignKey("TurystaUzytkownikId")]
        [InverseProperty("Wycieczka")]
        public Turysta TurystaUzytkownik { get; set; }
        [ForeignKey("Zweryfikowana")]
        [InverseProperty("Wycieczka")]
        public StatusWeryfikacji ZweryfikowanaNavigation { get; set; }
        [InverseProperty("Wycieczka")]
        public ICollection<Odcinek> Odcinek { get; set; }
        [InverseProperty("Wycieczka")]
        public ICollection<Weryfikacja> Weryfikacja { get; set; }
    }
}
