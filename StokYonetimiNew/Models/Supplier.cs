using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ShortName { get; set; }

        [Required, StringLength(200)]
        public string FullName { get; set; }

        [Required, StringLength(100)]
        public string TaxOffice { get; set; }           

        [Required, StringLength(50)]
        public string TaxNumber { get; set; }

        [Phone, StringLength(20)]
        public string LandlinePhone { get; set; }        

        [Phone, StringLength(20)]
        public string MobilePhone { get; set; }          

        [EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
