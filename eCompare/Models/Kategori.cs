namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kategori")]
    public partial class Kategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kategori()
        {
            Kategori1 = new HashSet<Kategori>();
            Urun = new HashSet<Urun>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(200)]
        public string video_yol { get; set; }

        [Required]
        [StringLength(200)]
        public string resim_yol { get; set; }

        public int? ust_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kategori> Kategori1 { get; set; }

        public virtual Kategori Kategori2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }
    }
}
