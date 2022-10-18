using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace FridgesWebApp.Models
{
    public class UserEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
