using System.ComponentModel.DataAnnotations;

namespace Users_Notebook.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imie jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imie może mieć maksymalnie 50 znaków.")]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(150, ErrorMessage = "Nazwisko może mieć maksymalnie 150 znaków.")]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        public string Data_urodzenia { get; set; }

        [Required(ErrorMessage = "Plec jest wymagana.")]
        public string Plec { get; set; }
        public string Atrybut { get; set; }
        public string Wartosc { get; set; }
    }
}
