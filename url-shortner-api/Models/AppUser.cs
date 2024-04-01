using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace url_shortner_api.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "First name required.")]
        public string? First { get; set; }
    }
}
