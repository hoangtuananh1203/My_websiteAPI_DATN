using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class Danhcho
    {
        [Key]
        public int DanhchoId { get; set; }
        [Required]
        [StringLength(50)]
        public string Doituong { get; set; }
        public ICollection<Diadiem> diadiems { get; set; } = new List<Diadiem> { };
    }
}
