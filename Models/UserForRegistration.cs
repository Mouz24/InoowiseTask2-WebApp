using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace FridgesWebApp.Models
{
    public class UserForRegistration
    {
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
