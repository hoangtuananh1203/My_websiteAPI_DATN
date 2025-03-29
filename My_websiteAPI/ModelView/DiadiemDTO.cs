using My_websiteAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.ModelView
{
    public class DiadiemDTO
    {
       
        public int DiadiemId { get; set; } // mã
        public string Tieude { get; set; }// Tên địa diểm
        public string Motangan { get; set; }// Mô tả ngắn
        public string Mocuadongcua { get; set; }// mocuadongcua
        public string Diachi { get; set; } // Dia chi
        public string DateOC { get; set; } // ngày mở và đóng
        public string Email { get; set; } // email
        public string SDT { get; set; } // SDT
        public int Gia { get; set; } // giá cả 
        public Boolean Tinhtrang { get; set; } // đóng == flase mở ==true
        public string Noidung { get; set; }
        public int TinhThanhId { get; set; }// Tỉnh
        public int LoaiHinhId { get; set; }// loại hình du lịch
        public int DanhchoId { get; set; }// loại hình du lịch
        public int Luotxem { get; set; }
        public int Loaisukien { get; set; }
        public string Imagemain { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string urlmap { get; set; }
    }
    public class DiadiemMV
    {

        public int DiadiemId { get; set; } // mã
        public string Tieude { get; set; }// Tên địa diểm
        public string Motangan { get; set; }// Mô tả ngắn
        public string Mocuadongcua { get; set; }// mocuadongcua
        public string Diachi { get; set; } // Dia chi
        public string DateOC { get; set; } // ngày mở và đóng
        public string Email { get; set; } // email
        public string SDT { get; set; } // SDT
        public int Gia { get; set; } // giá cả 
        public Boolean Tinhtrang { get; set; } // đóng == flase mở ==true
        public string Noidung { get; set; }
        public string TinhThanh { get; set; }// Tỉnh
        public string LoaiHinh { get; set; }// loại hình du lịch
        public string Danhcho { get; set; }// loại hình du lịch
        public int Luotxem { get; set; }
        public string Loaisukien { get; set; }
        public string Imagemain { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string urlmap { get; set; }
    }
    public class DiadiemMV1
    {

        public int DiadiemId { get; set; } // mã
        public string Tieude { get; set; }// Tên địa diểm
        public string Motangan { get; set; }// Mô tả ngắn
        public string Mocuadongcua { get; set; }// mocuadongcua
        public string Diachi { get; set; } // Dia chi
        public string DateOC { get; set; } // ngày mở và đóng
        public string Email { get; set; } // email
        public string SDT { get; set; } // SDT
        public int Gia { get; set; } // giá cả 
        public Boolean Tinhtrang { get; set; } // đóng == flase mở ==true
        public string Noidung { get; set; }
        public string TinhThanh { get; set; }// Tỉnh
        public string LoaiHinh { get; set; }// loại hình du lịch
        public string Danhcho { get; set; }// loại hình du lịch
        public int Luotxem { get; set; }
        public string Loaisukien { get; set; }
        public string Imagemain { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string urlmap { get; set; }
        public int TinhThanhId { get; set; }// Tỉnh
        public int LoaiHinhId { get; set; }// loại hình du lịch
        public int DanhchoId { get; set; }// loại hình du lịch
    
        public int LoaisukienId { get; set; }
    }
}
