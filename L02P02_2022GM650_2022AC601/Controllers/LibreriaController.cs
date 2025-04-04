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
    }
}
