using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace My_websiteAPI.Model
{
    public class Diadiem
    {
        [Key]
        public int DiadiemId { get; set; } // mã
        [Required]
        [StringLength(150)]
        public string Tieude { get; set; }// Tên địa diểm
        [Required]
   
        public string Motangan { get; set; }// Mô tả ngắn
        
        [StringLength(100)]
        public string Mocuadongcua { get; set; }// mocuadongcua
        [Required]
        [StringLength(500)]
        public string Diachi { get; set; } // Dia chi
        [Required]
        [StringLength(50)]
        public string DateOC { get; set; } // ngày mở và đóng
        
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng Email!")]
        public string Email { get; set; } // email
       
        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại!")]
        public string SDT { get; set; } // SDT
      
        public int Gia { get; set; } // giá cả 
        public Boolean Tinhtrang { get; set; } // đóng == flase mở ==true
        [Required]
        [Column(TypeName = "text")]
        public string Noidung { get; set; }
        [ForeignKey("TinhThanhId")]
        public int TinhThanhId { get; set; }// Tỉnh
        public TinhThanh? TinhThanh { get; set; }
        [ForeignKey("LoaiHinhId")]
        public int LoaiHinhId { get; set; }// loại hình du lịch
        public LoaiHinh? LoaiHinhDL { get; set; }
        [ForeignKey("DanhchoId")]
        public int DanhchoId { get; set; }// loại hình du lịch
        public Danhcho? Danhcho { get; set; }
        public int Luotxem {  get; set; }
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
        [Required]
  
        public string urlmap { get; set; }


    }
}
