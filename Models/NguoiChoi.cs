namespace GameWeb_XL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiChoi")]
    public partial class NguoiChoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiChoi()
        {
            BanBes = new HashSet<BanBe>();
            BanBes1 = new HashSet<BanBe>();
            LoiMoiKBs = new HashSet<LoiMoiKB>();
            LoiMoiKBs1 = new HashSet<LoiMoiKB>();
        }

        [Key]
        [StringLength(50)]
        public string Ten { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        public int? CapDo { get; set; }

        [StringLength(150)]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BanBe> BanBes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BanBe> BanBes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoiMoiKB> LoiMoiKBs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoiMoiKB> LoiMoiKBs1 { get; set; }
    }
}
