using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Przodownik
    {
        public Przodownik()
        {
            UprawnieniePrzodownika = new HashSet<UprawnieniePrzodownika>();
            Weryfikacja = new HashSet<Weryfikacja>();
            Wycieczka = new HashSet<Wycieczka>();
        }

        [Key]
        [Column("UzytkownikID")]
        public int UzytkownikId { get; set; }

        [ForeignKey("UzytkownikId")]
        [InverseProperty("Przodownik")]
        public Uzytkownik Uzytkownik { get; set; }
        [InverseProperty("PrzodownikUzytkownik")]
        public ICollection<UprawnieniePrzodownika> UprawnieniePrzodownika { get; set; }
        [InverseProperty("PrzodownikUzytkownik")]
        public ICollection<Weryfikacja> Weryfikacja { get; set; }
        [InverseProperty("PrzodownikUzytkownik")]
        public ICollection<Wycieczka> Wycieczka { get; set; }
    }
}
