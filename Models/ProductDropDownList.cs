using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace FridgesWebApp.Models
{
    public class ProductDropDownList
    {
            public ProductDropDownList()
            {
                ProductList = new List<SelectListItem>();
            }
       
            public List<SelectListItem> ProductList { get; set; }
    }
}
