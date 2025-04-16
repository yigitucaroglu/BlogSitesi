using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class RegisterViewModel
    {


        [Required]
        [Display(Name = "Adı")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
       
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfileImage { get; set; }
        
        

    }
}
