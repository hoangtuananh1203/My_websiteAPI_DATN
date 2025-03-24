using My_websiteAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_websiteAPI.Model
{
    public class LichsuTrainghiem
    {
        [Key]
        public int LichsuTrainghiemId { get; set; }

        public string UserId { get; set; } 
                                         
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public ICollection<itemLichsuTrainghiem> itemLichsuTrainghiems { get; set; } = new List<itemLichsuTrainghiem> { };

    }
}
