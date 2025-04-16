using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class BlogCreateViewModel
    {
        [Required(ErrorMessage = "Başlık zorunludur")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik boş bırakılamaz")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Kategori seçilmelidir")]
        public int? CategoryId { get; set; }

       
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public List<SelectListItem>? Categories { get; set; }
    }
}
