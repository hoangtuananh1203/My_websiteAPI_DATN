using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class BannerThree
    {
        [Key]
        public int BannerThreeId { get; set; }
        [Required]
        [StringLength(100)]
        public string Image1 { get; set; }
        [Required]
        [StringLength(100)]
        public string Image2 { get; set; }
        [Required]
        [StringLength(100)]
        public string Image3 { get; set; }
    }
}
