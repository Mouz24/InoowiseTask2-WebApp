using FridgesWebApp.Models;

namespace FridgesWebApp.ProductUpdateRepository
{
    public class ProductUpdateEntityRepository
    {
        public ProductUpdateEntity DefineFirstProduct(FridgeEntity fridge)
        {
            ProductUpdateEntity Product = new ProductUpdateEntity();
            
            if (fridge.FirstProduct == ProductList.Beef)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Beef",
                    Quantity = 0
                };
            }
            else if (fridge.FirstProduct == ProductList.Cheese)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Cheese",
                    Quantity = 0
                };
            }
            else if (fridge.FirstProduct == ProductList.Cucumber)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Cucumber",
                    Quantity = 0,
                };
            }
            else if (fridge.FirstProduct == ProductList.Egg)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Egg",
                    Quantity = 0,
                };
            }

            return Product;
        }

        public ProductUpdateEntity DefineSecondProduct(FridgeEntity fridge)
        {
            ProductUpdateEntity Product = new ProductUpdateEntity();

            if (fridge.SecondProduct == ProductList.Beef)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Beef",
                    Quantity = 0
                };
            }
            else if (fridge.SecondProduct == ProductList.Cheese)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Cheese",
                    Quantity = 0
                };
            }
            else if (fridge.SecondProduct == ProductList.Cucumber)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Cucumber",
                    Quantity = 0,
                };
            }
            else if (fridge.SecondProduct == ProductList.Egg)
            {
                Product = new ProductUpdateEntity
                {
                    Name = "Egg",
                    Quantity = 0,
                };
            }

            return Product;
        }

    }
}
