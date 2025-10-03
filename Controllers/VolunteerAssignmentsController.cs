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
    public class VolunteerAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<IdentityUser> _um;

        public VolunteerAssignmentsController(ApplicationDbContext ctx, UserManager<IdentityUser> um)
        {
            _ctx = ctx; _um = um;
        }

        // List the current user's assignments
        public async Task<IActionResult> My()
        {
            var uid = _um.GetUserId(User)!;
            var list = await _ctx.VolunteerAssignments
                .Include(a => a.VolunteerTask)
                .Where(a => a.UserId == uid)
                .OrderByDescending(a => a.AssignedAt)
                .ToListAsync();
            return View(list);
        }

        // Sign up the current user to a task
        public async Task<IActionResult> SignUp(int taskId)
        {
            var uid = _um.GetUserId(User)!;

            bool exists = await _ctx.VolunteerAssignments
                .AnyAsync(a => a.VolunteerTaskId == taskId && a.UserId == uid);

            if (!exists)
            {
                _ctx.VolunteerAssignments.Add(new VolunteerAssignment
                {
                    VolunteerTaskId = taskId,
                    UserId = uid
                });
                await _ctx.SaveChangesAsync();
            }
            return RedirectToAction(nameof(My));
        }

        // (Optional) Cancel sign-up
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var uid = _um.GetUserId(User)!;
            var a = await _ctx.VolunteerAssignments.FindAsync(id);
            if (a == null) return NotFound();
            if (a.UserId != uid && !User.IsInRole("Admin")) return Forbid();

            _ctx.VolunteerAssignments.Remove(a);
            await _ctx.SaveChangesAsync();
            return RedirectToAction(nameof(My));
        }
    }
}
