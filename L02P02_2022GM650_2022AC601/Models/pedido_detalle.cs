using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L02P02_2022GM650_2022AC601.Models
{
    public class pedido_detalle
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("ID Pedido")]
        [Required]
        public int id_pedido { get; set; }

        [DisplayName("ID Libro")]
        [Required]
        public int id_libro { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
