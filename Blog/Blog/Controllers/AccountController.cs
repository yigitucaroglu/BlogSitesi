using BlogProject.Models;
using BlogProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Alan: {key} - Hata: {error.ErrorMessage}");
                    }
                }

                return View(model);
            }
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState geçersiz");
                TempData["Error"] = "Model geçersiz";
              
                return View(model); 
            }
            string uniqueFileName = null;
            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfileImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }
            }


            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                PhoneNumber = model.PhoneNumber,
                ProfileImageUrl = uniqueFileName != null ? "/uploads/" + uniqueFileName : "/images/default-user.png",
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description); 
            }
            TempData["Error"] = "Kayıt başarısız oldu";
            return View(model);
             
        
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Giriş bilgileri hatalı.");
            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ProfileViewModel
            {
                FirstName = user.Name,
                Surname = user.Surname,
                CurrentImageUrl = user.ProfileImageUrl
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                user.Name = model.FirstName;
                user.Surname = model.Surname;

                if (model.ProfileImage != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfileImage.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfileImage.CopyToAsync(stream);
                    }

                    user.ProfileImageUrl = "/uploads/" + fileName;
                }

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Profile");
            }

            model.CurrentImageUrl = user.ProfileImageUrl;
            return View(model);
        }
    }
}