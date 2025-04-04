using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L02P02_2022GM650_2022AC601.Models
{
    public class clientes
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string nombre { get; set; }

        [DisplayName("Apellido")]
        [Required]
        public string apellido { get; set; }

        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [DisplayName("Dirección")]
        public string direccion { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
