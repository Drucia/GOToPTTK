using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class OdcinekPunktowany
    {
        public const int POINT_MAXIMUM = 1000;
        public OdcinekPunktowany()
        {
            Odcinek = new HashSet<Odcinek>();
        }

        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(1, POINT_MAXIMUM, ErrorMessage = "Liczba punktów musi być dodatnia")]
        public int Punkty { get; set; }
        [Column("PoczatekID")]
        public int PoczatekId { get; set; }
        [Column("KoniecID")]
        public int KoniecId { get; set; }
        [Column("WykazTrasID")]
        public int WykazTrasId { get; set; }
        [Column("GrupaGorskaID")]
        public int GrupaGorskaId { get; set; }

        [ForeignKey("GrupaGorskaId")]
        [InverseProperty("OdcinekPunktowany")]
        public GrupaGorska GrupaGorska { get; set; }
        [ForeignKey("KoniecId")]
        [InverseProperty("OdcinekPunktowanyKoniec")]
        public Miejsce Koniec { get; set; }
        [ForeignKey("PoczatekId")]
        [InverseProperty("OdcinekPunktowanyPoczatek")]
        public Miejsce Poczatek { get; set; }
        [ForeignKey("WykazTrasId")]
        [InverseProperty("OdcinekPunktowany")]
        public WykazTras WykazTras { get; set; }
        [InverseProperty("OdcinekPunktowany")]
        public ICollection<Odcinek> Odcinek { get; set; }


        public bool IsRouteEquivalent(OdcinekPunktowany route)
        {
            return (Id != route.Id && PoczatekId == route.PoczatekId && KoniecId == route.KoniecId);
        }
    }
}
