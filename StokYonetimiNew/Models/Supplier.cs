using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kısa Ad alanı zorunludur.")]
        [Display(Name = "Şirket Kısa Adı")]
        [StringLength(100)]
        public string ShortName { get; set; }

        [Required(ErrorMessage = "Tam Ad alanı zorunludur.")]
        [Display(Name = "Şirket Tam Adı")]
        [StringLength(200)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vergi Numarası alanı zorunludur.")]
        [Display(Name = "Vergi Numarası")]
        [StringLength(20)]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "İlçe alanı zorunludur.")]
        [Display(Name = "İlçe")]
        [StringLength(100)]
        public string District { get; set; }

        [Required(ErrorMessage = "Şehir alanı zorunludur.")]
        [Display(Name = "Şehir")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Tam Adres alanı zorunludur.")]
        [Display(Name = "Tam Adres")]
        [StringLength(500)]
        public string FullAddress { get; set; }

        [Required(ErrorMessage = "Sabit Telefon alanı zorunludur.")]
        [Display(Name = "Sabit Telefon")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(20)]
        public string LandlinePhone { get; set; }

        [Required(ErrorMessage = "Cep Telefonu alanı zorunludur.")]
        [Display(Name = "Cep Telefonu")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(20)]
        public string MobilePhone { get; set; }
    }
}