using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L02P02_2022GM650_2022AC601.Models
{
    public class comentarios_libros
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("ID Libro")]
        [Required]
        public int id_libro { get; set; }

        [DisplayName("Comentarios")]
        [Required]
        public string comentarios { get; set; }

        [DisplayName("Usuario")]
        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        [DisplayName("Fecha de Creación")]
        public DateTime created_at { get; set; } = DateTime.Now;
    }
}
