namespace GameWeb_XL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BanBe")]
    public partial class BanBe
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Ten1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Ten2 { get; set; }

        public DateTime? TG { get; set; }

        public virtual NguoiChoi NguoiChoi { get; set; }

        public virtual NguoiChoi NguoiChoi1 { get; set; }
    }
}
