using Fiorella.DAL;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fiorella.Areas.Admin.Controllers
{
    public class LayoutController : Controller
    {
        private readonly AppDbContext _context;
        public LayoutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var layouts = await _context.Layout.ToListAsync();

            return View(layouts);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var layoutI = await _context.Layout.FindAsync(id);
            if (layoutI == null)
            {
                return NotFound();
            }
            return View(layoutI);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Layout lyt)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _context.Layout.AddAsync(lyt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
