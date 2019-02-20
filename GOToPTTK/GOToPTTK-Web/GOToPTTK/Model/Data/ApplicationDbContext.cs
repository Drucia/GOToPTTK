using System;
using GOToPTTK.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GOToPTTK.Model.Entities
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Got> Got { get; set; }
        public virtual DbSet<GrupaGorska> GrupaGorska { get; set; }
        public virtual DbSet<Miejsce> Miejsce { get; set; }
        public virtual DbSet<NazwaGot> NazwaGot { get; set; }
        public virtual DbSet<Odcinek> Odcinek { get; set; }
        public virtual DbSet<OdcinekPunktowany> OdcinekPunktowany { get; set; }
        public virtual DbSet<OdcinekWłasny> OdcinekWłasny { get; set; }
        public virtual DbSet<Przodownik> Przodownik { get; set; }
        public virtual DbSet<RegionGorski> RegionGorski { get; set; }
        public virtual DbSet<StatusWeryfikacji> StatusWeryfikacji { get; set; }
        public virtual DbSet<Turysta> Turysta { get; set; }
        public virtual DbSet<UprawnieniePrzodownika> UprawnieniePrzodownika { get; set; }
        public virtual DbSet<Uzytkownik> Uzytkownik { get; set; }
        public virtual DbSet<Weryfikacja> Weryfikacja { get; set; }
        public virtual DbSet<Wycieczka> Wycieczka { get; set; }
        public virtual DbSet<WykazTras> WykazTras { get; set; }
        public virtual DbSet<Zdjecie> Zdjecie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GOToPTTK;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.Property(e => e.UzytkownikId).ValueGeneratedNever();
                entity.HasOne(d => d.Uzytkownik)
                    .WithOne(p => p.Administrator)
                    .HasForeignKey<Administrator>(d => d.UzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKAdministra650189");
            });

            modelBuilder.Entity<Got>(entity =>
            {
                entity.Property(e => e.NazwaGot).IsUnicode(false);

                entity.HasOne(d => d.NazwaGotNavigation)
                    .WithMany(p => p.Got)
                    .HasForeignKey(d => d.NazwaGot)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGOT908643");

                entity.HasOne(d => d.TurystaUzytkownik)
                    .WithMany(p => p.Got)
                    .HasForeignKey(d => d.TurystaUzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGOT822868");
            });

            modelBuilder.Entity<GrupaGorska>(entity =>
            {
                entity.Property(e => e.Nazwa).IsUnicode(false);

                entity.Property(e => e.RegionGorski).IsUnicode(false);

                entity.HasOne(d => d.RegionGorskiNavigation)
                    .WithMany(p => p.GrupaGorska)
                    .HasForeignKey(d => d.RegionGorski)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGrupaGorsk230312");
            });

            modelBuilder.Entity<Miejsce>(entity =>
            {
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.HasIndex(e => e.DlugoscGeograficzna)
                    .HasName("UQ__Miejsce__45197B236B2456E6")
                    .IsUnique();

                entity.HasIndex(e => e.SzerokoscGeograficzna)
                    .HasName("UQ__Miejsce__12F49CC6A756E497")
                    .IsUnique();

                entity.HasIndex(e => e.WysokoscNpm)
                    .HasName("UQ__Miejsce__F7023FAD77B153CA")
                    .IsUnique();

                entity.Property(e => e.Nazwa).IsUnicode(true);

                entity.Property(e => e.Opis).IsUnicode(true);
            });

            modelBuilder.Entity<NazwaGot>(entity =>
            {
                entity.Property(e => e.Nazwa)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Odcinek>(entity =>
            {
                entity.Property(e => e.Zweryfikowany).IsUnicode(false);

                entity.HasOne(d => d.OdcinekPunktowany)
                    .WithMany(p => p.Odcinek)
                    .HasForeignKey(d => d.OdcinekPunktowanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinek407257");

                entity.HasOne(d => d.OdcinekWłasny)
                    .WithMany(p => p.Odcinek)
                    .HasForeignKey(d => d.OdcinekWłasnyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinek419520");

                entity.HasOne(d => d.Wycieczka)
                    .WithMany(p => p.Odcinek)
                    .HasForeignKey(d => d.WycieczkaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinek473282");

                entity.HasOne(d => d.ZweryfikowanyNavigation)
                    .WithMany(p => p.Odcinek)
                    .HasForeignKey(d => d.Zweryfikowany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinek926146");
            });

            modelBuilder.Entity<OdcinekPunktowany>(entity =>
            {
                entity.HasOne(d => d.GrupaGorska)
                    .WithMany(p => p.OdcinekPunktowany)
                    .HasForeignKey(d => d.GrupaGorskaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekPun313090");

                entity.HasOne(d => d.Koniec)
                    .WithMany(p => p.OdcinekPunktowanyKoniec)
                    .HasForeignKey(d => d.KoniecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekPun124417");

                entity.HasOne(d => d.Poczatek)
                    .WithMany(p => p.OdcinekPunktowanyPoczatek)
                    .HasForeignKey(d => d.PoczatekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekPun52704");

                entity.HasOne(d => d.WykazTras)
                    .WithMany(p => p.OdcinekPunktowany)
                    .HasForeignKey(d => d.WykazTrasId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekPun996725");
            });

            modelBuilder.Entity<OdcinekWłasny>(entity =>
            {
                entity.HasOne(d => d.GrupaGorska)
                    .WithMany(p => p.OdcinekWłasny)
                    .HasForeignKey(d => d.GrupaGorskaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekWła40235");

                entity.HasOne(d => d.Koniec)
                    .WithMany(p => p.OdcinekWłasnyKoniec)
                    .HasForeignKey(d => d.KoniecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekWła141178");

                entity.HasOne(d => d.Poczatek)
                    .WithMany(p => p.OdcinekWłasnyPoczatek)
                    .HasForeignKey(d => d.PoczatekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOdcinekWła35943");
            });

            modelBuilder.Entity<Przodownik>(entity =>
            {
                entity.Property(e => e.UzytkownikId).ValueGeneratedNever();

                entity.HasOne(d => d.Uzytkownik)
                    .WithOne(p => p.Przodownik)
                    .HasForeignKey<Przodownik>(d => d.UzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPrzodownik403906");
            });

            modelBuilder.Entity<RegionGorski>(entity =>
            {
                entity.Property(e => e.Nazwa)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<StatusWeryfikacji>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Turysta>(entity =>
            {
                entity.Property(e => e.UzytkownikId).ValueGeneratedNever();

                entity.HasOne(d => d.Uzytkownik)
                    .WithOne(p => p.Turysta)
                    .HasForeignKey<Turysta>(d => d.UzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTurysta427905");
            });

            modelBuilder.Entity<UprawnieniePrzodownika>(entity =>
            {
                entity.HasOne(d => d.GrupaGorska)
                    .WithMany(p => p.UprawnieniePrzodownika)
                    .HasForeignKey(d => d.GrupaGorskaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUprawnieni777839");

                entity.HasOne(d => d.PrzodownikUzytkownik)
                    .WithMany(p => p.UprawnieniePrzodownika)
                    .HasForeignKey(d => d.PrzodownikUzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUprawnieni19650");
            });

            modelBuilder.Entity<Uzytkownik>(entity =>
            {
                entity.HasIndex(e => e.Login)
                    .HasName("UQ__Uzytkown__5E55825B3C5CCF6E")
                    .IsUnique();

                entity.Property(e => e.Haslo).IsUnicode(false);

                entity.Property(e => e.Imie).IsUnicode(false);

                entity.Property(e => e.Login).IsUnicode(false);

                entity.Property(e => e.Nazwisko).IsUnicode(false);
            });

            modelBuilder.Entity<Weryfikacja>(entity =>
            {
                entity.Property(e => e.StatusWeryfikacji).IsUnicode(false);

                entity.Property(e => e.Uwagi).IsUnicode(false);

                entity.HasOne(d => d.PrzodownikUzytkownik)
                    .WithMany(p => p.Weryfikacja)
                    .HasForeignKey(d => d.PrzodownikUzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWeryfikacj99940");

                entity.HasOne(d => d.StatusWeryfikacjiNavigation)
                    .WithMany(p => p.Weryfikacja)
                    .HasForeignKey(d => d.StatusWeryfikacji)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWeryfikacj345300");

                entity.HasOne(d => d.Wycieczka)
                    .WithMany(p => p.Weryfikacja)
                    .HasForeignKey(d => d.WycieczkaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWeryfikacj807523");
            });

            modelBuilder.Entity<Wycieczka>(entity =>
            {
                entity.Property(e => e.Zweryfikowana).IsUnicode(false);

                entity.HasOne(d => d.Got)
                    .WithMany(p => p.Wycieczka)
                    .HasForeignKey(d => d.Gotid)
                    .HasConstraintName("FKWycieczka26684");

                entity.HasOne(d => d.PrzodownikUzytkownik)
                    .WithMany(p => p.Wycieczka)
                    .HasForeignKey(d => d.PrzodownikUzytkownikId)
                    .HasConstraintName("FKWycieczka485657");

                entity.HasOne(d => d.TurystaUzytkownik)
                    .WithMany(p => p.Wycieczka)
                    .HasForeignKey(d => d.TurystaUzytkownikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWycieczka953986");

                entity.HasOne(d => d.ZweryfikowanaNavigation)
                    .WithMany(p => p.Wycieczka)
                    .HasForeignKey(d => d.Zweryfikowana)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKWycieczka11533");
            });

            modelBuilder.Entity<Zdjecie>(entity =>
            {
                entity.Property(e => e.Podpis).IsUnicode(false);

                entity.HasOne(d => d.Odcinek)
                    .WithMany(p => p.Zdjecie)
                    .HasForeignKey(d => d.OdcinekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKZdjecie193815");
            });
        }
    }
}
