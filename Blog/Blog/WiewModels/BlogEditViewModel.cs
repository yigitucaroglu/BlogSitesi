using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class BlogEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; } 


        [Required(ErrorMessage = "Kategori seçilmelidir")]
        public int? CategoryId { get; set; }

        public List<SelectListItem>? Categories { get; set; }
    }
}