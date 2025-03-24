using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class LoaiHinh
    {
        [Key]
        public int LoaiHinhId { get; set; }
        [Required]
        [StringLength(50)]
        public string TenLoai { get; set; }
        public ICollection<Diadiem> diadiems { get; set; } = new List<Diadiem> { };
    }
}
