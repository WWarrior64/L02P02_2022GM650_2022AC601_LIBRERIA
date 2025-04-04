using L02P02_2022GM650_2022AC601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022GM650_2022AC601.Controllers
{
    public class AutorController : Controller
    {
        private readonly libreriaDbContext _context;
        public AutorController(libreriaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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
