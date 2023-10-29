namespace QuanLiTinTuc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuDe")]
    public partial class ChuDe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuDe()
        {
            TinTucs = new HashSet<TinTuc>();
        }

        [Key]
        [StringLength(20)]
        public string MaChuDe { get; set; }

        [Required]
        [StringLength(100)]
        public string TenChuDe { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        public int? SoLuongBaiViet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TinTuc> TinTucs { get; set; }
    }
}
