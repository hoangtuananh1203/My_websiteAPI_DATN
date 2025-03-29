using My_websiteAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.ModelView
{
    public class SuKienTintucDTO
    {
     
        public int SukienId { get; set; } // mã
       
        public string Tieude { get; set; }// Tên địa diểm  
       
        public string Motangan { get; set; }// Mota ngan
       
        public string Diachi { get; set; } // Dia chi
        
        public DateTime DateOpen { get; set; }
      
        public DateTime DateClose { get; set; }
        public int Gia { get; set; } // giá cả 
       
        public string SDT { get; set; } // SDT
     
        public string Mota1 { get; set; }
       
        public int TinhThanhId { get; set; }// Tỉnh
      
        public int LoaiHinhId { get; set; }// loại hình du lịch
       
        public int DanhchoId { get; set; }// loại hình du lịch
      

        public int Loaisukien { get; set; }
        public string Imagemain { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
    }
    public class SuKienTintucMV
    {

        public int SukienId { get; set; } // mã

        public string Tieude { get; set; }// Tên địa diểm  

        public string Motangan { get; set; }// Mota ngan

        public string Diachi { get; set; } // Dia chi

        public DateTime DateOpen { get; set; }

        public DateTime DateClose { get; set; }
        public int Gia { get; set; } // giá cả 

        public string SDT { get; set; } // SDT

        public string Mota1 { get; set; }

        public string TinhThanh { get; set; }// Tỉnh

        public string LoaiHinh { get; set; }// loại hình du lịch

        public string Danhcho { get; set; }// loại hình du lịch


        public string Loaisukien { get; set; }
        public string Imagemain { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
    }
    public class SuKienTintucMV1
    {
        public int SukienId { get; set; } // mã

        public string Tieude { get; set; }// Tên địa diểm  

        public string Motangan { get; set; }// Mota ngan

        public string Diachi { get; set; } // Dia chi

        public DateTime DateOpen { get; set; }

        public DateTime DateClose { get; set; }
        public int Gia { get; set; } // giá cả 

        public string SDT { get; set; } // SDT

        public string Mota1 { get; set; }

        public string TinhThanh { get; set; }// Tỉnh

        public string LoaiHinh { get; set; }// loại hình du lịch

        public string Danhcho { get; set; }// loại hình du lịch


        public string Loaisukien { get; set; }
        public string Imagemain { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public int TinhThanhId { get; set; }// Tỉnh

        public int LoaiHinhId { get; set; }// loại hình du lịch

        public int DanhchoId { get; set; }// loại hình du lịch


        public int LoaisukienId { get; set; }

    }
}
