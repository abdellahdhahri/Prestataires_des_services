namespace ServicesPlomberie.Models
{
    public class Specialite
    {
        

        public int Id { get; set; }
        public string nom { get; set; }
        public int code { get; set; }
        public  int serviceId { get; set; }
        public Service service { get; set; }
    }

}
