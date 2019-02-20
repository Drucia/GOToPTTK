using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class StatusWeryfikacji
    {
        public StatusWeryfikacji()
        {
            Odcinek = new HashSet<Odcinek>();
            Weryfikacja = new HashSet<Weryfikacja>();
            Wycieczka = new HashSet<Wycieczka>();
        }

        [Key]
        [StringLength(20)]
        public string Status { get; set; }

        [InverseProperty("ZweryfikowanyNavigation")]
        public ICollection<Odcinek> Odcinek { get; set; }
        [InverseProperty("StatusWeryfikacjiNavigation")]
        public ICollection<Weryfikacja> Weryfikacja { get; set; }
        [InverseProperty("ZweryfikowanaNavigation")]
        public ICollection<Wycieczka> Wycieczka { get; set; }
    }
}
