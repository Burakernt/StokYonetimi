using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class MainCategory
    {
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }   // "01", "02"…

        [Required, MaxLength(100)]
        public string Name { get; set; }

    }
}
