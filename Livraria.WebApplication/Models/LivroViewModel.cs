using System.ComponentModel.DataAnnotations;

namespace Livraria.WebApplication.Models
{
    public class LivroViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(150)]
        [Display(Name = "Autor(a)")]
        public string Autor { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}
