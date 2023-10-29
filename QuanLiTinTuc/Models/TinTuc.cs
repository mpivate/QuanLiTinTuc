namespace QuanLiTinTuc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinTuc")]
    public partial class TinTuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TinTuc()
        {
            BinhLuans = new HashSet<BinhLuan>();
        }

        [Key]
        [StringLength(20)]
        public string MaTinTuc { get; set; }

        [StringLength(255)]
        public string TieuDe { get; set; }

        [StringLength(1000)]
        public string NoiDung { get; set; }

        public DateTime? NgayDang { get; set; }

        [StringLength(100)]
        public string TacGia { get; set; }

        [StringLength(255)]
        public string AnhDaiDien { get; set; }

        [StringLength(20)]
        public string MaChuDe { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public int? BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        public virtual ChuDe ChuDe { get; set; }
    }
}
