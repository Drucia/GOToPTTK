using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Zdjecie
    {
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Podpis { get; set; }
        [Required]
        [Column(TypeName = "image")]
        public byte[] Obraz { get; set; }
        [Column("OdcinekID")]
        public int OdcinekId { get; set; }

        [ForeignKey("OdcinekId")]
        [InverseProperty("Zdjecie")]
        public Odcinek Odcinek { get; set; }
    }
}
