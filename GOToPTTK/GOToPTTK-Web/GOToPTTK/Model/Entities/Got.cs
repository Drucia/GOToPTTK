using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    [Table("GOT")]
    public partial class Got
    {
        public Got()
        {
            Wycieczka = new HashSet<Wycieczka>();
        }

        [Column("ID")]
        public int Id { get; set; }
        public bool Ukonczona { get; set; }
        public TimeSpan? RokZakonczenia { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DataZakonczenia { get; set; }
        public int AktualnePunkty { get; set; }
        [Required]
        [Column("NazwaGOT")]
        [StringLength(255)]
        public string NazwaGot { get; set; }
        [Column("TurystaUzytkownikID")]
        public int TurystaUzytkownikId { get; set; }

        [ForeignKey("NazwaGot")]
        [InverseProperty("Got")]
        public NazwaGot NazwaGotNavigation { get; set; }
        [ForeignKey("TurystaUzytkownikId")]
        [InverseProperty("Got")]
        public Turysta TurystaUzytkownik { get; set; }
        [InverseProperty("Got")]
        public ICollection<Wycieczka> Wycieczka { get; set; }
    }
}
