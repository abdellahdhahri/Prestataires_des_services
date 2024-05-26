namespace ServicesPlomberie.Models
{
    public class Post
    {
        public string PostId { get; set; }
        public String Description { get; set; }

        public string Id { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
