using L02P02_2022GM650_2022AC601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022GM650_2022AC601.Controllers
{
    public class LibreriaController : Controller
    {
        private readonly libreriaDbContext _context;
        public LibreriaController(libreriaDbContext context)
        {
            _context = context;
        }

        public IActionResult Autor()
        {
            var listadoAutores = _context.autores
                .Select(a => new
                {
                    Id = a.id,
                    Nombre = a.autor
                })
                .ToList();

            ViewData["listadoAutores"] = listadoAutores;

            return View();
        }

        public IActionResult ListadoLibrosPorAutor(int autorId)
        {
            var autor = _context.autores.FirstOrDefault(a => a.id == autorId);
            if (autor == null)
            {
                return NotFound("Autor no encontrado.");
            }

            // Obtener los libros del autor con datos estructurados
            var libros = _context.libros
                .Where(l => l.id_autor == autorId)
                .Select(l => new
                {
                    Id = l.id,
                    Nombre = l.nombre // 📌 Asegura que el nombre sea correcto
                })
                .ToList();

            ViewData["NombreAutor"] = autor.autor;
            ViewData["LibrosDelAutor"] = libros; // 📌 Pasamos la lista correctamente

            return View("ListadoLibrosPorAutor");
        }

        public IActionResult ComentariosPorLibro(int idLibro)
        {
            var libro = _context.libros.FirstOrDefault(l => l.id == idLibro);
            if (libro == null)
            {
                return NotFound("Libro no encontrado.");
            }

            var autor = _context.autores.FirstOrDefault(a => a.id == libro.id_autor);
            if (autor == null)
            {
                return NotFound("Autor no encontrado.");
            }

            // Obtener comentarios del libro
            var comentarios = _context.comentarios_libros
                .Where(c => c.id_libro == idLibro)
                .Select(c => new
                {
                    usuario = c.usuario,
                    comentarios = c.comentarios,
                    created_at = c.created_at
                })
                .ToList();

            ViewData["NombreAutor"] = autor.autor;
            ViewData["LibroNombre"] = libro.nombre;
            ViewData["listadoComentarios"] = comentarios;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EscribirLibros(int idLibro, string usuario, string comentario)
        {
            // Verificar si el libro existe
            var libro = _context.libros.FirstOrDefault(l => l.id == idLibro);
            if (libro == null)
            {
                return NotFound("Libro no encontrado.");
            }

            // Verificar si el comentario no está vacío
            if (string.IsNullOrEmpty(comentario))
            {
                ModelState.AddModelError("", "El comentario no puede estar vacío.");
                return RedirectToAction("ComentariosPorLibro", new { idLibro = idLibro });
            }

            // Mostrar los valores recibidos para depuración
            Console.WriteLine($"Usuario: {usuario}, Comentario: {comentario}");

            // Crear una nueva entidad de comentario
            var nuevoComentario = new comentarios_libros
            {
                id_libro = idLibro,
                usuario = usuario,
                comentarios = comentario,
                created_at = DateTime.Now
            };

            // Guardar el comentario en la base de datos
            _context.comentarios_libros.Add(nuevoComentario);
            _context.SaveChanges();

            // Redirigir al usuario a la vista de los comentarios del libro
            return RedirectToAction("ComentariosPorLibro", new { idLibro = idLibro });
        }



        public IActionResult ComentarioAdicionado()
        {
            
            return View();
        }



    }
}
