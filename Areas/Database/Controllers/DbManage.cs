using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreMVC.Data;
using NetCoreMVC.Models;

namespace NetCoreMVC.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-management/[action]")]
    public class DbManage : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbManage(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: DbManage
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _context.Database.EnsureDeletedAsync();

            StatusMessage = success ? "Database deleted successfully" : "Database deletion failed";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _context.Database.MigrateAsync();

            StatusMessage = "Database created successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedData()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            foreach (var role in roleNames)
            {
                var roleName = (string)role.GetRawConstantValue();
                var roleFound = await _roleManager.FindByNameAsync(roleName);
                if (roleFound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Kiểm tra user admin
            var userAdmin = await _userManager.FindByEmailAsync("admin1@example.com");
            if (userAdmin == null)
            {
                userAdmin = new AppUser()
                {
                    UserName = "admin1",
                    Email = "admin1@example.com",
                    EmailConfirmed = true
                };

                var createUserResult = await _userManager.CreateAsync(userAdmin, "Admin@123");
                if (!createUserResult.Succeeded)
                {
                    foreach (var error in createUserResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                    StatusMessage = "Error creating admin user";
                    return RedirectToAction(nameof(Index));
                }
            }

            // Kiểm tra vai trò trước khi thêm user vào
            var adminRoleExists = await _roleManager.RoleExistsAsync(RoleName.Administrator);
            if (!adminRoleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(RoleName.Administrator));
            }

            // Kiểm tra user có trong vai trò chưa
            var isInRole = await _userManager.IsInRoleAsync(userAdmin, RoleName.Administrator);
            if (!isInRole)
            {
                await _userManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
            }
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            Console.WriteLine($"User: {user.UserName}");
            Console.WriteLine("Roles: " + string.Join(", ", roles));

            StatusMessage = "Data seeded successfully";
            return RedirectToAction(nameof(Index));
        }

        
    }
}
