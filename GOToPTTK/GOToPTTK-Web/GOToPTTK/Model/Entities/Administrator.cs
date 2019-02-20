using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Administrator
    {
        [Key]
        [Column("UzytkownikID")]
        public int UzytkownikId { get; set; }

        [ForeignKey("UzytkownikId")]
        [InverseProperty("Administrator")]
        public Uzytkownik Uzytkownik { get; set; }
    }
}
