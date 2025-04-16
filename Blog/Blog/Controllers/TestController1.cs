using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models;
using System.Threading.Tasks;

namespace BlogProject.Controllers
{
    public class TestController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public TestController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateTestUser()
        {
            var user = new AppUser
            {
                UserName = "testuser",
                Email = "test@example.com"
            };

            var result = await _userManager.CreateAsync(user, "Test123*");

            if (result.Succeeded)
                return Content("Kullanıcı başarıyla oluşturuldu!");

            return Content("Kullanıcı oluşturulamadı: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}