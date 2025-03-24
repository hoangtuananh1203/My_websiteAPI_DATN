using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_websiteAPI.Model
{
    public class Donggop
    {
        [Key]
        public int DonggopId { get; set; }
        [Required]
        [StringLength(150)]
        public string Tieude { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public string Noidung { get; set; }
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng Email!")]
        public string Email { get; set; } // email  
        [Phone(ErrorMessage = "Vui lòng nhập đúng định dạng số điện thoại!")]
        public string SDT { get; set; } // SDT
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int trangthai { get; set; }

    }
}
