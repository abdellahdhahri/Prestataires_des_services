using System.ComponentModel.DataAnnotations;

namespace ServicesPlomberie.Models
{
    public class Service
    {
        [Key]
        public int id { get; set; }
        public int code { get; set; }
        public string nom { get; set; }

        public ICollection <Specialite> Specialite { get; set; }
    }
}
