using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class CustomerTeam
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Department { get; set; } 

        [Required, MaxLength(100)]
        public string TeamName { get; set; }  

    }

}
 