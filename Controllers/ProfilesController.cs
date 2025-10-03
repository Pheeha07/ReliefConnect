using System;
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
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly UserManager<IdentityUser> _um;

        public ProfilesController(ApplicationDbContext ctx, UserManager<IdentityUser> um)
        {
            _ctx = ctx;
            _um = um;
        }

        // Creates a Profile automatically if missing, then redirects to Edit
        public async Task<IActionResult> My()
        {
            var uid = _um.GetUserId(User);
            if (uid is null) return Challenge();
            var id = Guid.Parse(uid);

            var profile = await _ctx.Profiles.FindAsync(id);
            if (profile == null)
            {
                var user = await _um.GetUserAsync(User);
                profile = new Profile
                {
                    Id = id,
                    Email = user?.Email ?? "",
                    Phone = user?.PhoneNumber,
                    FullName = ""
                };
                _ctx.Profiles.Add(profile);
                await _ctx.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id });
        }

        // GET: /Profiles/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var uid = _um.GetUserId(User);
            if (uid is null) return Challenge();
            if (Guid.Parse(uid) != id && !User.IsInRole("Admin")) return Forbid();

            var model = await _ctx.Profiles.FindAsync(id);
            return model == null ? NotFound() : View(model);
        }

        // POST: /Profiles/Edit/{id}
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Profile model)
        {
            var uid = _um.GetUserId(User);
            if (uid is null) return Challenge();
            if (id != model.Id) return NotFound();
            if (Guid.Parse(uid) != id && !User.IsInRole("Admin")) return Forbid();
            if (!ModelState.IsValid) return View(model);

            // optional: sync Identity phone
            var user = await _um.FindByIdAsync(uid);
            if (user != null)
            {
                user.PhoneNumber = model.Phone;
                await _um.UpdateAsync(user);
            }

            _ctx.Update(model);
            await _ctx.SaveChangesAsync();
            TempData["ok"] = "Profile updated.";
            return RedirectToAction(nameof(My));
        }

        // Admin-only list
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _ctx.Profiles.OrderBy(p => p.FullName).ToListAsync();
            return View(list);
        }

        // Admin-only details
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(Guid id)
        {
            var p = await _ctx.Profiles.FindAsync(id);
            return p == null ? NotFound() : View(p);
        }
    }
}
