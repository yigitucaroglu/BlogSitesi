using BlogProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BlogProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using BlogProject.Repositories;
using System.Reflection.Metadata;
using BlogProject.Helpers;

namespace BlogProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeRepository _likeRepository;

        public BlogController(IBlogRepository blogRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, UserManager<AppUser> userManager)
        {
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var blogs = categoryId.HasValue
                ? await _blogRepository.GetByCategoryAsync(categoryId.Value)
                : await _blogRepository.GetAllAsync();


          
            var topBlogs = await _blogRepository.GetTopViewedBlogsAsync(10);
            
            var viewModel = new BlogListViewModel
            {
                Blogs = blogs,
                Categories = categories,
                SelectedCategoryId = categoryId,
                TopBlogs = topBlogs
            };

            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryRepository.GetAllAsync())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(model);
            }

            string? imagePath = "/images/default-blog.png";
            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/uploads/" + fileName;
            }

            var userId = _userManager.GetUserId(User);
            

            var blog = new BlogPost
            {
                Title = model.Title,
                Content = model.Content, // Summernote içeriği
                PublishedDate = DateTime.Now,
                ImageUrl = imagePath,
                CategoryId = model.CategoryId,
                UserId = userId
            };

            await _blogRepository.AddAsync(blog);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var model = new BlogCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(model);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            var currentUserId = _userManager.GetUserId(User);

            if (blog == null || blog.UserId != currentUserId)
                return Forbid();

            var categories = await _categoryRepository.GetAllAsync();

            var model = new BlogEditViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                ImageUrl = blog.ImageUrl,
                CategoryId = blog.CategoryId,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(BlogEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = (await _categoryRepository.GetAllAsync())
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();
                return View(model);
            }

            var blog = await _blogRepository.GetByIdAsync(model.Id);
            if (blog == null || blog.UserId != _userManager.GetUserId(User))
                return Forbid();

            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.CategoryId = model.CategoryId;
            blog.PublishedDate = DateTime.Now;

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                blog.ImageUrl = "/uploads/" + fileName;
            }


            await _blogRepository.UpdateAsync(blog);
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);

            if (blog == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            if (blog.UserId != currentUserId)
                return Forbid(); 

            await _blogRepository.DeleteAsync(blog);
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyBlogs()
        {
            var userId = _userManager.GetUserId(User);
            var blogs = await _blogRepository.GetByUserIdAsync(userId);

            return View(blogs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
                return NotFound();

            blog.ViewCount++;
            await _blogRepository.UpdateAsync(blog);

            var comments = await _commentRepository.GetCommentsByBlogIdAsync(id);
            var commentTree = CommentHelper.BuildCommentTree(comments);
            var commentTreeViewModel = commentTree.Select(c => MapToViewModel(c)).ToList();
            ViewBag.BlogId = blog.Id;
            ViewBag.BlogOwnerId = blog.UserId;

            var viewModel = new BlogDetailsViewModel
            {
                Blog = blog,
                LikeCount = blog.Likes?.Count(l => l.IsLiked) ?? 0,
                DislikeCount = blog.Likes?.Count(l => !l.IsLiked) ?? 0,
                CommentTree = commentTreeViewModel
            };

            return View(viewModel);
        }




        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(int BlogId, string Content, int? ParentCommentId)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                TempData["CommentError"] = "Yorum boş olamaz.";
                return RedirectToAction("Details", new { id = BlogId });
            }

            var userId = _userManager.GetUserId(User);

            var comment = new Comment
            {
                BlogId = BlogId,
                Content = Content,
                UserId = userId,
                CreatedAt = DateTime.Now,
                ParentCommentId = ParentCommentId
            };

            await _commentRepository.AddAsync(comment);

            return RedirectToAction("Details", new { id = BlogId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Like(int id)
        {
            var userId = _userManager.GetUserId(User);
            var existing = await _likeRepository.GetUserLikeAsync(id, userId);

            if (existing == null)
            {
                var like = new Like
                {
                    BlogId = id,
                    UserId = userId,
                    IsLiked = true
                };
                await _likeRepository.AddLikeAsync(like);
            }
            else
            {
                existing.IsLiked = true;
                await _likeRepository.UpdateLikeAsync(existing);
            }

            return RedirectToAction("Details", new { id });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Dislike(int id)
        {
            var userId = _userManager.GetUserId(User);
            var existing = await _likeRepository.GetUserLikeAsync(id, userId);

            if (existing == null)
            {
                var dislike = new Like
                {
                    BlogId = id,
                    UserId = userId,
                    IsLiked = false
                };
                await _likeRepository.AddLikeAsync(dislike);
            }
            else
            {
                existing.IsLiked = false;
                await _likeRepository.UpdateLikeAsync(existing);
            }

            return RedirectToAction("Details", new { id });
        }


        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return RedirectToAction("Index");

            var results = await _blogRepository.SearchAsync(query);

            ViewBag.SearchQuery = query;
            return View("SearchResults", results); 
        }
        private CommentViewModel MapToViewModel(Comment comment)
        {
            return new CommentViewModel
            {
                Id = comment.Id,
                Content = comment.Content,
                UserName = comment.User?.UserName,
                CreatedAt = comment.CreatedAt,
                ParentCommentId = comment.ParentCommentId,
                Replies = comment.Replies?.Select(r => MapToViewModel(r)).ToList() ?? new List<CommentViewModel>()
            };
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditComment(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            var userId = _userManager.GetUserId(User);

            if (comment == null || comment.UserId != userId)
                return Forbid();

            return View(comment); // ayrı View oluşturacağız
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditComment(int id, string content)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            var userId = _userManager.GetUserId(User);

            if (comment == null || comment.UserId != userId)
                return Forbid();

            comment.Content = content;
            comment.UpdatedAt = DateTime.Now;
            await _commentRepository.UpdateAsync(comment);

            return RedirectToAction("Details", new { id = comment.BlogId });
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);
            var blog = await _blogRepository.GetByIdAsync(comment.BlogId);

            
            if (comment.UserId != currentUserId && blog.UserId != currentUserId)
            {
                return Forbid();
            }

            await _commentRepository.DeleteAsync(comment);
            return RedirectToAction("Details", new { id = comment.BlogId });
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var imageUrl = Url.Content("~/uploads/" + fileName);
                return Json(new { location = imageUrl }); 
            }

            return BadRequest();
        }



    }

}