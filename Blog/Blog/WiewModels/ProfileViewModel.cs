public class ProfileViewModel
{
    public string FirstName { get; set; }
    public string Surname { get; set; }

    public IFormFile ProfileImage { get; set; }

    public string CurrentImageUrl { get; set; }
}
