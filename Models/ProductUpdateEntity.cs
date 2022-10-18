using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace FridgesWebApp.Models
{
    public class ProductUpdateEntity
    {
        [Required(ErrorMessage = "Name is a required field.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Name is not valid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Quantity is a required field.")]
        public int Quantity { get; set; }

        public int? Default_quantity { get; set; }
    }
}
