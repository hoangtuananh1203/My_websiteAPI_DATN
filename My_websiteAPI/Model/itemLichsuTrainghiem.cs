using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class itemLichsuTrainghiem
    {
        [Key]
        public int itemLichsuTrainghiemId { get; set; }
        [ForeignKey("LichsuTrainghiemId")]
        public int LichsuTrainghiemId { get; set; }
        public LichsuTrainghiem? LichsuTrainghiem { get; set; }

        [ForeignKey("DiadiemId")]
        public int DiadiemId { get; set; }
        public Diadiem? Diadiem { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
