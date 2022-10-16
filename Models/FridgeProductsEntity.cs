using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace FridgesWebApp.Models
{
    public class FridgeProductsEntity
    {
        public Guid Fridge_Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Name is not valid")]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Owner name is not valid")]
        public string? Owner_name { get; set; }

        public Guid Model_Id { get; set; }

        [Required(ErrorMessage = "Fridge name of model is a required field.")]
        public string NameOfModel { get; set; }

        public int? Year { get; set; }

        public Guid Product_Id { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$", ErrorMessage = "Product name is not valid")]
        public string Product { get; set; }

        [Required(ErrorMessage = "Product quantity is a required field.")]
        public int Quantity { get; set; }
    }
}
