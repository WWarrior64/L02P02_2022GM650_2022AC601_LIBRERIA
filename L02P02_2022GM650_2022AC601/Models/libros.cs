using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L02P02_2022GM650_2022AC601.Models
{
    public class libros
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Nombre")]
        [Required]
        public string nombre { get; set; }

        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        [DisplayName("URL Imagen")]
        public string url_imagen { get; set; }

        [DisplayName("ID Autor")]
        [Required]
        public int id_autor { get; set; }

        [DisplayName("ID Categoría")]
        [Required]
        public int id_categoria { get; set; }

        [DisplayName("Precio")]
        [Required]
        [Range(0, 999999.99)]
        public decimal precio { get; set; }

        [DisplayName("Estado")]
        [StringLength(1)]
        public string estado { get; set; }
    }
}
