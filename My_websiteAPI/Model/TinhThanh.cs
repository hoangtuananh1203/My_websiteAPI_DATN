using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class TinhThanh
    {
        [Key]
        public int TinhThanhId { get; set; }
        [Required]
        [StringLength(50)]
        public string TenTinh { get; set; }
        public ICollection<Diadiem> Diadiems { get; set; } = new List<Diadiem> { };
        public ICollection<SukienTintuc> SukienTintucs { get; set; } = new List<SukienTintuc> { };
    }
}
