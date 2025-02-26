using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreMVC.Models;

namespace NetCoreMVC.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-management/[action]")]
    public class DbManage : Controller
    {
        private readonly AppDbContext _context;

        public DbManage(AppDbContext context)
        {
            _context = context;
        }

        // GET: DbManage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb(){
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync(){
            var success= await _context.Database.EnsureDeletedAsync();

            StatusMessage = success ? "Database deleted successfully" : "Database deletion failed";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Migrate(){
            await _context.Database.MigrateAsync();

            StatusMessage = "Database created successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
