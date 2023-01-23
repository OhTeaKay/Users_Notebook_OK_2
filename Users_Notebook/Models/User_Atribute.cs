using System.ComponentModel.DataAnnotations;

namespace Users_Notebook.Models
{
    public class UserAttribute
    {
        [Key]
        public int Id { get; set; }
        public int Osoba_id { get; set; }
        public string Atrybut { get; set; }
        public string Wartosc { get; set; }
    }
}