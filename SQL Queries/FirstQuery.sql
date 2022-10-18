USE Fridges
SELECT products.Name,  Quantity, fridge.Name, fridge_model.NameOfModel, fridge.Owner_name FROM fridge_products, products, fridge, fridge_model
WHERE Product_id = products.id AND Fridge_id = fridge.id AND fridge.Model_id = fridge_model.id
AND SUBSTRING(fridge.Name, 1, 1) = 'A'