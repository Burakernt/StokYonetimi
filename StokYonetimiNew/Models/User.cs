using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public enum UserRole
    {
        None = 0,
        Admin = 1,
        Reporter = 2
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
