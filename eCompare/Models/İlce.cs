namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class İlce
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public İlce()
        {
            Adres = new HashSet<Adres>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string ad { get; set; }

        public int il_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adres> Adres { get; set; }

        public virtual İl İl { get; set; }
    }
}
