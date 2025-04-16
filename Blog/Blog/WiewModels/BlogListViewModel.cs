using BlogProject.Models;
using System.Collections.Generic;

namespace BlogProject.ViewModels
{
    public class BlogListViewModel
    {
        public List<BlogPost> Blogs { get; set; }
        public List<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }

        public List<BlogPost> TopBlogs { get; set; } 
    }
}
