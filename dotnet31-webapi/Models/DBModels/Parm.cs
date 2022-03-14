using System.ComponentModel.DataAnnotations;

#nullable disable

namespace cmes_webapi.Models
{
    public partial class Parm
    {
        [Key]
        public string IdParm { get; set; }

        public string ValorStr { get; set; }

        public int ValorInt { get; set; }
    }
}
