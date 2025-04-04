using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L02P02_2022GM650_2022AC601.Models
{
    public class autores
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Autor")]
        [Required]
        [StringLength(200)]
        public string autor { get; set; }
    }
}
