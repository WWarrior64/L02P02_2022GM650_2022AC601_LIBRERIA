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
          
            var autor = _context.autores
                                .FirstOrDefault(a => a.id == autorId);

            ViewData["NombreAutor"] = autor?.autor ?? "Autor no encontrado";

            
            var librosDelAutor = _context.libros
                .Where(l => l.id_autor == autorId && l.estado == "A")
                .Select(l => new
                {
                    l.id,
                    l.nombre
                })
                .ToList();

            ViewData["LibrosDelAutor"] = librosDelAutor;

            return View("ListadoLibrosPorAutor");
        }





    }
}
