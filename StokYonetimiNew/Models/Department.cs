using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}