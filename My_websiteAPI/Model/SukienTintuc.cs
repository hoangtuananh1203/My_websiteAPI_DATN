using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class SukienTintuc
    {

        [Key]
        public int SukienId { get; set; } // mã
        [Required]
        [StringLength(150)]
        public string Tieude { get; set; }// Tên địa diểm  
        [Required]


        public string Motangan { get; set; }// Mota ngan
        [Required]
        
        public string Diachi { get; set; } // Dia chi
        [Column(TypeName = "date")]
        public DateTime DateOpen { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateClose { get; set; }
        public int Gia { get; set; } // giá cả 
        [Required]
        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại!")]
        public string SDT { get; set; } // SDT
        [Required]
        [Column(TypeName = "text")]
        public string Mota1 { get; set; }
        [ForeignKey("TinhThanhId")]
        public int TinhThanhId { get; set; }// Tỉnh
        public TinhThanh? TinhThanh { get; set; }
        [ForeignKey("LoaiHinhId")]
        public int LoaiHinhId { get; set; }// loại hình du lịch
        public LoaiHinh? LoaiHinhDL { get; set; }
        [ForeignKey("DanhchoId")]
        public int DanhchoId { get; set; }// loại hình du lịch
        public Danhcho? Danhcho { get; set; }
        
        
        public int Loaisukien { get; set; }
        [Required]
        [StringLength(50)]
        public string Imagemain { get; set; }
        [Required]
        [StringLength(50)]
        public string Image1 { get; set; }
        [Required]
        [StringLength(50)]
        public string Image2 { get; set; }
        [Required]
        [StringLength(50)]
        public string Image3 { get; set; }
        [Required]
        [StringLength(50)]
        public string Image4 { get; set; }
        [Required]
        [StringLength(50)]
        public string Image5 { get; set; }
    }
}
