namespace QuanLiTinTuc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        [StringLength(20)]
        public string MaBinhLuan { get; set; }

        [StringLength(20)]
        public string MaTinTuc { get; set; }

        [StringLength(1000)]
        public string NoiDung { get; set; }

        [StringLength(100)]
        public string NguoiBinhLuan { get; set; }

        public DateTime? ThoiGianDang { get; set; }

        public virtual TinTuc TinTuc { get; set; }
    }
}
