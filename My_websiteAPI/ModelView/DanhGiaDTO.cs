using My_websiteAPI.Data;
using My_websiteAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.ModelView
{
    public class DanhGiaDTO
    {
        public int DanhgiaId { get; set; }
        public int Diem { get; set; }
      
        public string Noidung { get; set; }
       
        public DateTime Ngay_add { get; set; }

        public int DiadiemId { get; set; }
        public string UserId { get; set; }

    }
    public class DanhGiaMV
    {
        public int DanhgiaId { get; set; }
        public int Diem { get; set; }

        public string Noidung { get; set; }

        public DateTime Ngay_add { get; set; }

        public string Diadiem { get; set; }
        public int Iddiadiem { get; set; }
        public string UserId { get; set; }
        public string Nameuser { get; set; }

    }
}
