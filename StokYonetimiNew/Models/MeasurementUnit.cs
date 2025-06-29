using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class MeasurementUnit
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
