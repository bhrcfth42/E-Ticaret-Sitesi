namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sepet")]
    public partial class Sepet
    {
        public int id { get; set; }

        public int kullanci_id { get; set; }

        public int urun_id { get; set; }

        public int urun_adet { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
