using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class Miejsce
    {
        public Miejsce()
        {
            OdcinekPunktowanyKoniec = new HashSet<OdcinekPunktowany>();
            OdcinekPunktowanyPoczatek = new HashSet<OdcinekPunktowany>();
            OdcinekWłasnyKoniec = new HashSet<OdcinekWłasny>();
            OdcinekWłasnyPoczatek = new HashSet<OdcinekWłasny>();
        }

        [Column("ID")]
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:F7}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(10, 7)")]
        [Display(Name = "Szerokość geograficzna")]
        public decimal SzerokoscGeograficzna { get; set; }
        [DisplayFormat(DataFormatString = "{0:F7}", ApplyFormatInEditMode = true)]
        [Column(TypeName = "decimal(10, 7)")]
        [Display(Name = "Długość geograficzna")]
        public decimal DlugoscGeograficzna { get; set; }
        [Column("WysokoscNPM")]
        [Display(Name = "Wysokość")]
        public float WysokoscNpm { get; set; }

        [StringLength(255)]
        public string Nazwa { get; set; }
        [StringLength(255)]
        public string Opis { get; set; }
        [Display(Name = "Godzina otwarcia")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? GodzinaOtwarcia { get; set; }
        [Display(Name = "Godzina zamknięcia")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? GodzinaZamkniecia { get; set; }

        [InverseProperty("Koniec")]
        public ICollection<OdcinekPunktowany> OdcinekPunktowanyKoniec { get; set; }
        [InverseProperty("Poczatek")]
        public ICollection<OdcinekPunktowany> OdcinekPunktowanyPoczatek { get; set; }
        [InverseProperty("Koniec")]
        public ICollection<OdcinekWłasny> OdcinekWłasnyKoniec { get; set; }
        [InverseProperty("Poczatek")]
        public ICollection<OdcinekWłasny> OdcinekWłasnyPoczatek { get; set; }

        public bool IsEqual(Miejsce place)
        {
            return place.Id != Id && place.SzerokoscGeograficzna == SzerokoscGeograficzna && place.DlugoscGeograficzna == DlugoscGeograficzna && place.WysokoscNpm == WysokoscNpm;
        }
    }
}
