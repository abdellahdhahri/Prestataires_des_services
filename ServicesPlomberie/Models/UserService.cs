using ServicesPlomberie.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesPlomberie.Models
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        
        public string UserId { get; set; }

        public Utilisateur Utilisateur { get; set; }

       
        public int id { get; set; }

        public Service Service { get; set; }

        internal Task AddServiceToUserAsync(string userId, int serviceId)
        {
            throw new NotImplementedException();
        }
    }
}
