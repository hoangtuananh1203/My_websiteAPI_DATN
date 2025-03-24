using My_websiteAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_websiteAPI.Model
{
    public class ListAwait
    {
        [Key]
        public int ListAwaitId { get; set; }

        public string UserId { get; set; } 
                                          
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public ICollection<itemListAwait> itemListAwaits { get; set; } = new List<itemListAwait> { };

    }
}
