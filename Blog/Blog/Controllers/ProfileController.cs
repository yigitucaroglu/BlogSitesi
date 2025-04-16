using BlogProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IWebHostEnvironment _env;

    public ProfileController(UserManager<AppUser> userManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(AppUser model, IFormFile? ProfileImage)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        user.Name = model.Name;
        user.Surname = model.Surname;
        user.UserName = model.UserName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
       

        if (ProfileImage != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(ProfileImage.FileName);
            var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
            using var stream = new FileStream(path, FileMode.Create);
            await ProfileImage.CopyToAsync(stream);
            user.ProfileImageUrl = "/uploads/" + fileName;
        }

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
            return RedirectToAction("Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View("Index", user);
    }
}
