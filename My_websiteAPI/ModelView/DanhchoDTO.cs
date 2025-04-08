using System.ComponentModel.DataAnnotations;

namespace My_websiteAPI.ModelView
{
    public class DanhchoDTO
    {
        public int DanhchoId { get; set; }
      
        public string Doituong { get; set; }
    }
    public class DanhchoMV
    {
        public int DanhchoId { get; set; }
        public int sodiadiem { get; set; }

        public string Doituong { get; set; }
    }
}
