using Microsoft.AspNetCore.Identity;

namespace MyProFile.Data.Models
{
    public class User : IdentityUser<int>
    {
        // или "teacher", "admin", "guest"
        public string Role { get; set; } = "student"; 

        // Връзка с ученици (ако този User е teacher)
        public ICollection<Student>? Mentees { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
