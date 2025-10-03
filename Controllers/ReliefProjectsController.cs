using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReliefConnect.Data;
using ReliefConnect.Models;

namespace ReliefConnect.Controllers
{
    public class ReliefProjectsController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public ReliefProjectsController(ApplicationDbContext ctx) => _ctx = ctx;

        public async Task<IActionResult> Index()
            => View(await _ctx.ReliefProjects.OrderByDescending(p => p.DateCreated).ToListAsync());

        public async Task<IActionResult> Details(int id)
        {
            var m = await _ctx.ReliefProjects.FindAsync(id);
            return m == null ? NotFound() : View(m);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ReliefProject m)
        {
            if (!ModelState.IsValid) return View(m);
            _ctx.Add(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var m = await _ctx.ReliefProjects.FindAsync(id);
            return m == null ? NotFound() : View(m);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, ReliefProject m)
        {
            if (id != m.Id) return NotFound();
            if (!ModelState.IsValid) return View(m);
            _ctx.Update(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _ctx.ReliefProjects.FindAsync(id);
            return m == null ? NotFound() : View(m);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _ctx.ReliefProjects.FindAsync(id);
            if (m != null) _ctx.ReliefProjects.Remove(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
