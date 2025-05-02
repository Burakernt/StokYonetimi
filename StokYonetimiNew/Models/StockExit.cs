using System.ComponentModel.DataAnnotations;

namespace StokYonetimiNew.Models
{
    public class StockExit
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }       // Çıkış Tarihi

        // FK → Ürün
        public int ProductId { get; set; }
        public Material Product { get; set; }

        // FK → Müşteri/Ekip
        public int CustomerTeamId { get; set; }
        public CustomerTeam CustomerTeam { get; set; }

        [Required]
        public decimal Quantity { get; set; }    // Çıkış Miktarı
    }
}
