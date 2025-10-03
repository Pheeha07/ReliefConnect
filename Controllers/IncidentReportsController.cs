using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReliefConnect.Data;
using ReliefConnect.Models;

namespace ReliefConnect.Controllers
{
    [Authorize]
    public class IncidentReportsController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<IdentityUser> _um;

        public IncidentReportsController(ApplicationDbContext ctx, UserManager<IdentityUser> um)
        {
            _ctx = ctx; _um = um;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
            => View(await _ctx.IncidentReports
                              .OrderByDescending(i => i.ReportedAt).ToListAsync());

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var m = await _ctx.IncidentReports.FindAsync(id);
            return m == null ? NotFound() : View(m);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport m)
        {
            if (!ModelState.IsValid) return View(m);
            m.ReporterUserId = _um.GetUserId(User);
            _ctx.IncidentReports.Add(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var m = await _ctx.IncidentReports.FindAsync(id);
            return m == null ? NotFound() : View(m);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = await _ctx.IncidentReports.FindAsync(id);
            if (m != null) _ctx.IncidentReports.Remove(m);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
