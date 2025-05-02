
using System;
using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class MaterialExit
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int MaterialId { get; set; }
        public Material? Material { get; set; }   

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int CustomerTeamId { get; set; }
        public CustomerTeam? CustomerTeam { get; set; }  
    }
}
