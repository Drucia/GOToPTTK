using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    [Table("NazwaGOT")]
    public partial class NazwaGot
    {
        public NazwaGot()
        {
            Got = new HashSet<Got>();
        }

        [Key]
        [StringLength(255)]
        public string Nazwa { get; set; }
        public int WymaganePunkty { get; set; }

        [InverseProperty("NazwaGotNavigation")]
        public ICollection<Got> Got { get; set; }
    }
}
