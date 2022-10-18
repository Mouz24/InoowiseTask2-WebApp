USE Fridges
SELECT SUM(Products) AS KindsOfProducts, t.Owner_name, t.FridgeName, t.NameOfModel FROM
   (SELECT COUNT(Product_id) AS Products, Fridge_id,fridge.Name AS FridgeName, fridge_model.NameOfModel, 
    fridge.Owner_name, products.Name
    FROM fridge_products, products, fridge, fridge_model
    WHERE Product_id = products.id AND Fridge_id = fridge.id AND fridge.Model_id = fridge_model.id AND Quantity > Default_quantity
    GROUP BY fridge.Name, fridge_model.NameOfModel, fridge.Owner_name, products.Name, Fridge_id
	) t
GROUP BY t.Fridge_id, t.Owner_name, t.FridgeName, t.NameOfModel