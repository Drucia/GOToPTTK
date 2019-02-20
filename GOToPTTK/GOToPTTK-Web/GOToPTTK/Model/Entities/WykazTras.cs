using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class WykazTras
    {
        public WykazTras()
        {
            OdcinekPunktowany = new HashSet<OdcinekPunktowany>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DataObowiazywania { get; set; }
        public bool Zatwierdzony { get; set; }
        public bool Obowiazuje { get; set; }

        [InverseProperty("WykazTras")]
        public ICollection<OdcinekPunktowany> OdcinekPunktowany { get; set; }
    }
}
