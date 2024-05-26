using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ServicesPlomberie.Models
{
    [Authorize(Roles ="admin")]
    public class Utilisateur :IdentityUser
        
    {
        public Boolean prestataire { get; set; }
        [Required, MaxLength(100)]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? region { get; set; }
        public string? genre { get; set; }
        public string? service { get; set; }
        public string? souservice { get; set; }
        public string? picture { get; set; }
        public List<Post> posts { get; set; }
        public Utilisateur() {
            posts = new List<Post>(); 

        }








    }
}
