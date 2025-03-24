using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.Model
{
    public class itemListAwait
    {
        [Key]
        public int itemListAwaitId { get; set; }
        [ForeignKey("ListAwaitId")]
        public int ListAwaitId { get; set; }
        public ListAwait? ListAwait { get; set; }

        [ForeignKey("DiadiemId")]
        public int DiadiemId { get; set; }
        public Diadiem? Diadiem { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
    }
}
