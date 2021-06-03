namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Sepet = new HashSet<Sepet>();
            Siparis = new HashSet<Siparis>();
            Urun = new HashSet<Urun>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string ad { get; set; }

        [Required]
        [StringLength(50)]
        public string soyad { get; set; }

        [Required]
        [StringLength(50)]
        public string mail { get; set; }

        [Required]
        [StringLength(50)]
        public string parola { get; set; }

        [Column(TypeName = "image")]
        public byte[] resim { get; set; }

        public int adres_id { get; set; }

        public virtual Adres Adres { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sepet> Sepet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }
    }
}
