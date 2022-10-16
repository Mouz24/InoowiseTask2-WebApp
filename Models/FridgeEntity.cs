using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace FridgesWebApp.Models
{
    public class FridgeEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Name is not valid")]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Owner name is not valid")]
        public string? Owner_name { get; set; }

        public Guid Model_Id { get; set; }

        [Required(ErrorMessage = "Fridge name of model is a required field.")]
        public string NameOfModel { get; set; }

        public int? Year { get; set; }

        [Required(ErrorMessage = "You didn't choose products")]
        public ProductList FirstProduct { get; set; }
        
        [Required(ErrorMessage = "You didn't choose products")]
        public ProductList SecondProduct { get; set; }

    }
}
