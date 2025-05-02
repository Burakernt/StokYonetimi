using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{

    public class Unit
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }  

        public ICollection<Material> Products { get; set; }
    }
}
