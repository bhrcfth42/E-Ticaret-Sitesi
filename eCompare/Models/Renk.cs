namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Renk")]
    public partial class Renk
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Renk()
        {
            Renk1 = new HashSet<Renk>();
            Urun = new HashSet<Urun>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string adi { get; set; }

        [Required]
        [StringLength(50)]
        public string kod { get; set; }

        public int? ust_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Renk> Renk1 { get; set; }

        public virtual Renk Renk2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }
    }
}
