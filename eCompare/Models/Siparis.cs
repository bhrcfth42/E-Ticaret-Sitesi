namespace eCompare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Siparis
    {
        public int id { get; set; }

        public int kullanici_id { get; set; }

        public int urun_id { get; set; }

        public int adres_id { get; set; }

        public int adet { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime tarih { get; set; }

        public virtual Adres Adres { get; set; }

        public virtual Kullanici Kullanici { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
