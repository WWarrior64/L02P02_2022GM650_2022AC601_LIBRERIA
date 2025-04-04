using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L02P02_2022GM650_2022AC601.Models
{
    public class pedido_encabezado
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("ID Cliente")]
        [Required]
        public int id_cliente { get; set; }

        [DisplayName("Cantidad de Libros")]
        [Required]
        public int cantidad_libros { get; set; }

        [DisplayName("Total")]
        [Required]
        [Range(0, 999999.99)]
        public decimal total { get; set; }
    }
}
