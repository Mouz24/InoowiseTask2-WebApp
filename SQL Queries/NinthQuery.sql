USE Fridges
SELECT fridge.Name, fridge_model.NameOfModel, fridge.Owner_name, products.Name, Quantity, products.Default_quantity 
FROM fridge_products, products, fridge, fridge_model
WHERE Product_id = products.id AND Fridge_id = fridge.id AND fridge.Model_id = fridge_model.id
AND Quantity > products.Default_quantity