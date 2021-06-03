using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace eCompare.Models
{
    public partial class eCompareDatabase : DbContext
    {
        public eCompareDatabase()
            : base("name=eCompareDatabase")
        {
        }

        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Beden> Beden { get; set; }
        public virtual DbSet<İl> İl { get; set; }
        public virtual DbSet<İlce> İlce { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Renk> Renk { get; set; }
        public virtual DbSet<Resim> Resim { get; set; }
        public virtual DbSet<Sepet> Sepet { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<Video> Video { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adres>()
                .Property(e => e.detay)
                .IsUnicode(false);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Kullanici)
                .WithRequired(e => e.Adres)
                .HasForeignKey(e => e.adres_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Adres)
                .HasForeignKey(e => e.adres_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Beden>()
                .HasMany(e => e.Beden1)
                .WithOptional(e => e.Beden2)
                .HasForeignKey(e => e.ust_id);

            modelBuilder.Entity<Beden>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.Beden)
                .HasForeignKey(e => e.beden_id);

            modelBuilder.Entity<İl>()
                .HasMany(e => e.Adres)
                .WithRequired(e => e.İl)
                .HasForeignKey(e => e.il_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<İl>()
                .HasMany(e => e.İlce)
                .WithRequired(e => e.İl)
                .HasForeignKey(e => e.il_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<İlce>()
                .HasMany(e => e.Adres)
                .WithRequired(e => e.İlce)
                .HasForeignKey(e => e.ilce_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Kategori1)
                .WithOptional(e => e.Kategori2)
                .HasForeignKey(e => e.ust_id);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.Urun)
                .WithRequired(e => e.Kategori)
                .HasForeignKey(e => e.kategori_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Sepet)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.kullanci_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.kullanici_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Urun)
                .WithRequired(e => e.Kullanici)
                .HasForeignKey(e => e.kullanici_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Renk>()
                .HasMany(e => e.Renk1)
                .WithOptional(e => e.Renk2)
                .HasForeignKey(e => e.ust_id);

            modelBuilder.Entity<Renk>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.Renk)
                .HasForeignKey(e => e.renk_id);

            modelBuilder.Entity<Resim>()
                .HasMany(e => e.Resim1)
                .WithOptional(e => e.Resim2)
                .HasForeignKey(e => e.ust_id);

            modelBuilder.Entity<Resim>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.Resim)
                .HasForeignKey(e => e.resim_id);

            modelBuilder.Entity<Urun>()
                .Property(e => e.detay)
                .IsUnicode(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.Sepet)
                .WithRequired(e => e.Urun)
                .HasForeignKey(e => e.urun_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Urun)
                .HasForeignKey(e => e.urun_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Video>()
                .Property(e => e.yol)
                .IsFixedLength();

            modelBuilder.Entity<Video>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.Video)
                .HasForeignKey(e => e.video_id);

            modelBuilder.Entity<Video>()
                .HasMany(e => e.Video1)
                .WithOptional(e => e.Video2)
                .HasForeignKey(e => e.ust_id);
        }
    }
}
