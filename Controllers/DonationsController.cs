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
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DonationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var query = _context.Donations
                .OrderByDescending(d => d.Date)
                .AsQueryable();

            if (!User.IsInRole("Admin"))
            {
                var uid = _userManager.GetUserId(User);
                query = query.Where(d => d.UserId == uid);
            }

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.Items) // remove if you don't use DonationItem
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null) return NotFound();
            if (!User.IsInRole("Admin") && donation.UserId != _userManager.GetUserId(User)) return Forbid();

            return View(donation);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation model)
        {
            if (!ModelState.IsValid) return View(model);

            model.UserId = _userManager.GetUserId(User);
            _context.Donations.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.Items) // remove if you don't use DonationItem
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donation == null) return NotFound();
            if (!User.IsInRole("Admin") && donation.UserId != _userManager.GetUserId(User)) return Forbid();

            return View(donation);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Donation model)
        {
            if (id != model.Id) return NotFound();
            if (!User.IsInRole("Admin") && model.UserId != _userManager.GetUserId(User)) return Forbid();
            if (!ModelState.IsValid) return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            return donation == null ? NotFound() : View(donation);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken, Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // OPTIONAL: only if you added DonationItem support
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(int donationId, string description, int quantity = 1)
        {
            var donation = await _context.Donations.FindAsync(donationId);
            if (donation == null) return NotFound();
            if (!User.IsInRole("Admin") && donation.UserId != _userManager.GetUserId(User)) return Forbid();

            _context.DonationItems.Add(new DonationItem
            {
                DonationId = donationId,
                Description = description,
                Quantity = quantity
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = donationId });
        }
    }
}
