using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Uzytkownik
    {
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Login { get; set; }
        [Required]
        [StringLength(255)]
        public string Haslo { get; set; }
        [Required]
        [StringLength(255)]
        public string Imie { get; set; }
        [Required]
        [StringLength(255)]
        public string Nazwisko { get; set; }

        [InverseProperty("Uzytkownik")]
        public Administrator Administrator { get; set; }
        [InverseProperty("Uzytkownik")]
        public Przodownik Przodownik { get; set; }
        [InverseProperty("Uzytkownik")]
        public Turysta Turysta { get; set; }
    }
}
