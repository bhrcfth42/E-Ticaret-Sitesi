namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Urun")]
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            Sepet = new HashSet<Sepet>();
            Siparis = new HashSet<Siparis>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string adi { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [AllowHtml]
        public string detay { get; set; }

        public double fiyat { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime tarih { get; set; }

        public int stok { get; set; }

        public int kullanici_id { get; set; }

        public int? video_id { get; set; }

        public int? resim_id { get; set; }

        public int kategori_id { get; set; }

        public int? beden_id { get; set; }

        public int? renk_id { get; set; }

        public virtual Beden Beden { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Renk Renk { get; set; }

        public virtual Resim Resim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }

        public virtual Video Video { get; set; }
    }
}
