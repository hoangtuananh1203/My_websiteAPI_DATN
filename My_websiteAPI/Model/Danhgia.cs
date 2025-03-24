using My_websiteAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_websiteAPI.Model
{
    public class Danhgia
    {
        [Key]
        public int DanhgiaId { get; set; }
        [Required]
        public int Diem { get; set; }
        [Required]
        [StringLength(300)]
        public string Noidung { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Ngay_add { get; set; }
        
        [ForeignKey("DiadiemId")]
        public int DiadiemId { get; set; }
        public Diadiem? Diadiem { get; set; }
        public string UserId { get; set; } 
                                           
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

    }
}
