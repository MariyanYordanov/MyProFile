using Microsoft.AspNetCore.Identity;

namespace MyProFile.Data.Models
{
    public class User : IdentityUser<int>
    {
        public string Role { get; set; } = "student";
        public ICollection<Student>? Mentees { get; set; }
        public string FullName { get; set; } = string.Empty;
    }
}
