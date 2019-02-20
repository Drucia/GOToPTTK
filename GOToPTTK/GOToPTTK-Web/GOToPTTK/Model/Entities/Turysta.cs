using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Turysta
    {
        public Turysta()
        {
            Got = new HashSet<Got>();
            Wycieczka = new HashSet<Wycieczka>();
        }

        [Key]
        [Column("UzytkownikID")]
        public int UzytkownikId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DataUrodzenia { get; set; }
        public bool? CzyNiepelnosprawny { get; set; }
        public bool Najmlodszy { get; set; }

        [ForeignKey("UzytkownikId")]
        [InverseProperty("Turysta")]
        public Uzytkownik Uzytkownik { get; set; }
        [InverseProperty("TurystaUzytkownik")]
        public ICollection<Got> Got { get; set; }
        [InverseProperty("TurystaUzytkownik")]
        public ICollection<Wycieczka> Wycieczka { get; set; }
    }
}
