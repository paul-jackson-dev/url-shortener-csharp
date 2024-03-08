using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace url_shortner_api.Dtos
{
    public class RegistrationDto
    {
        [Required(ErrorMessage = "First name required.")]
        public string? First { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [Length(5, 200, ErrorMessage = "Password must be at least 5 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Verfication password required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        //public bool PasswordsMatch()
        //{
        //    if (Password.IsNullOrEmpty()) { } // pass and return false
        //    else if (Password.Equals(VerifyPassword)) { return true; }
        //    return false;
        //}
    }
}
