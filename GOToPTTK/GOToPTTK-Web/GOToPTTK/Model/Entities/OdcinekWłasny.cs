using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOToPTTK.Model.Entities
{
    public partial class OdcinekWłasny
    {
        public OdcinekWłasny()
        {
            Odcinek = new HashSet<Odcinek>();
        }

        [Column("ID")]
        public int Id { get; set; }
        public int Punkty { get; set; }
        public int Odleglosc { get; set; }
        public float RoznicaPoziomow { get; set; }
        [Column("PoczatekID")]
        public int PoczatekId { get; set; }
        [Column("KoniecID")]
        public int KoniecId { get; set; }
        [Column("GrupaGorskaID")]
        public int GrupaGorskaId { get; set; }

        [ForeignKey("GrupaGorskaId")]
        [InverseProperty("OdcinekWłasny")]
        public GrupaGorska GrupaGorska { get; set; }
        [ForeignKey("KoniecId")]
        [InverseProperty("OdcinekWłasnyKoniec")]
        public Miejsce Koniec { get; set; }
        [ForeignKey("PoczatekId")]
        [InverseProperty("OdcinekWłasnyPoczatek")]
        public Miejsce Poczatek { get; set; }
        [InverseProperty("OdcinekWłasny")]
        public ICollection<Odcinek> Odcinek { get; set; }
    }
}
