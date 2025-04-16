using System.Diagnostics;
using BlogProject.Models;
using BlogProject.Repositories.Interfaces;
using BlogProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogRepository _blogRepository;

        public HomeController(ICategoryRepository categoryRepository, IBlogRepository blogRepository)
        {
            _categoryRepository = categoryRepository;
            _blogRepository = blogRepository;
        }


        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var blogs = categoryId.HasValue
                ? await _blogRepository.GetByCategoryAsync(categoryId.Value)
                : await _blogRepository.GetAllAsync();

            var viewModel = new BlogListViewModel
            {
                Blogs = blogs,
                Categories = categories,
                SelectedCategoryId = categoryId
            };

            return View(viewModel);
        }


        // ?? Gizlilik Sayfasý
        public IActionResult Privacy()
        {
            return View();
        }

        // ?? Hata Sayfasý
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
