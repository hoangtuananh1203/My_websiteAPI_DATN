using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.ModelView
{
    public class LoaiHinhDTO
    {
      
        public int LoaiHinhId { get; set; }
    
        public string TenLoai { get; set; }
    }
    public class LoaiHinhMV
    {

        public int LoaiHinhId { get; set; }

        public string TenLoai { get; set; }
        public int sodiadiem { get; set; }
        
    }
}
