using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReliefConnect.Data;
using ReliefConnect.Models;

namespace ReliefConnect.Controllers
{
    public class VolunteerTasksController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public VolunteerTasksController(ApplicationDbContext ctx) => _ctx = ctx;

        [AllowAnonymous]
        public async Task<IActionResult> Index()
            => View(await _ctx.VolunteerTasks
                .OrderBy(t => t.StartTime).ToListAsync());

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var t = await _ctx.VolunteerTasks.FindAsync(id);
            return t == null ? NotFound() : View(t);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(VolunteerTask m)
        {
            if (!ModelState.IsValid) return View(m);
            _ctx.Add(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var t = await _ctx.VolunteerTasks.FindAsync(id);
            return t == null ? NotFound() : View(t);
        }

        [HttpPost, ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, VolunteerTask m)
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
            var t = await _ctx.VolunteerTasks.FindAsync(id);
            return t == null ? NotFound() : View(t);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var t = await _ctx.VolunteerTasks.FindAsync(id);
            if (t != null) _ctx.VolunteerTasks.Remove(t);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
