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

            var libros = _context.libros
                .Where(l => l.id_autor == autorId)
                .Select(l => new
                {
                    Id = l.id,
                    Nombre = l.nombre
                })
                .ToList();

            ViewData["NombreAutor"] = autor.autor;
            ViewData["LibrosDelAutor"] = libros; 

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

            var comentarios = _context.comentarios_libros
                .Where(c => c.id_libro == idLibro)
                .Select(c => new
                {
                    usuario = c.usuario,
                    comentarios = c.comentarios,
                    created_at = c.created_at
                })
                .ToList();
            var ultimoComentario = _context.comentarios_libros
            .OrderByDescending(c => c.id)
            .FirstOrDefault();

            int nuevoComentarioId = (ultimoComentario != null) ? ultimoComentario.id + 1 : 1;

            ViewData["NuevoComentarioId"] = nuevoComentarioId;
            ViewData["LibroId"] = idLibro;
            ViewData["NombreAutor"] = autor.autor;
            ViewData["LibroNombre"] = libro.nombre;
            ViewData["listadoComentarios"] = comentarios;

            return View();
        }

        [HttpPost]
        public IActionResult EscribirLibros(int idcomentario, int idLibro, string usuario, string comentario)
        {
            var libro = _context.libros.FirstOrDefault(l => l.id == idLibro);
            if (libro == null)
            {
                return NotFound("Libro no encontrado.");
            }

            if (string.IsNullOrEmpty(comentario))
            {
                return RedirectToAction("ComentariosPorLibro", new { idLibro = idLibro });
            }

            var autor = _context.autores.FirstOrDefault(a => a.id == libro.id_autor);
            if (autor == null)
            {
                return NotFound("Autor no encontrado.");
            }

            var nuevoComentario = new comentarios_libros
            {
                id = idcomentario,
                id_libro = idLibro,
                usuario = usuario,
                comentarios = comentario,
                created_at = DateTime.Now
            };

            _context.comentarios_libros.Add(nuevoComentario);
            _context.SaveChanges();

            return RedirectToAction("ComentarioAdicionado", new { idLibro = idLibro, idComentario = idcomentario }); 
        }


        public IActionResult ComentarioAdicionado(int idLibro, int idComentario)
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
            var ultimoComentario = _context.comentarios_libros
            .OrderByDescending(c => c.id)
            .FirstOrDefault();


            var comentarioAgregado = _context.comentarios_libros
            .FirstOrDefault(c => c.id == idComentario);

            ViewData["LibroId"] = idLibro;
            ViewData["NombreAutor"] = autor.autor;
            ViewData["LibroNombre"] = libro.nombre;
            ViewData["listadoComentarios"] = comentarios;
            ViewData["UltimoComentarioUsuario"] = comentarioAgregado?.usuario;
            ViewData["UltimoComentarioTexto"] = comentarioAgregado?.comentarios;
            ViewData["UltimoComentarioFecha"] = comentarioAgregado?.created_at;
            return View();
        }

    }
}
