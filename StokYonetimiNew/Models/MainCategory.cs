using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using StokYonetimiNew.Models;

namespace StokYonetimiNew.Models
{
    public class MainCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kod alanı zorunludur.")]
        [Display(Name = "Kod")]
        [StringLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Ana Kategori Adı alanı zorunludur.")]
        [Display(Name = "Ana Kategori Adı")]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation property
        //public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}