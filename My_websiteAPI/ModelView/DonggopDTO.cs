using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.ModelView
{
    public class DonggopDTO
    {
        
        public int DonggopId { get; set; }
       
    
        public string Tieude { get; set; }
     
        public string Name { get; set; }
       
        public string Noidung { get; set; }
        public string Email { get; set; } // email  
        public string SDT { get; set; } // SDT
        public DateTime Date { get; set; }
        public int trangthai { get; set; }
    }
}
