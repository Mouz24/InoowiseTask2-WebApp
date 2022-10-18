USE Fridges
DECLARE @FridgeId uniqueidentifier
SET @FridgeId = (SELECT u.Fridge_id FROM(SELECT TOP 1 SUM(Products) AS KindsOfProducts,  t.Owner_name, t.FridgeName, t.NameOfModel, t.Fridge_id FROM
   (SELECT COUNT(Product_id) AS Products, Fridge_id, fridge.Name AS FridgeName, fridge_model.NameOfModel, 
   fridge.Owner_name, products.Name AS ProductName
    FROM fridge_products, products, fridge, fridge_model
    WHERE Product_id = products.id AND Fridge_id = fridge.id AND fridge.Model_id = fridge_model.id
    GROUP BY fridge.Name, fridge_model.NameOfModel, fridge.Owner_name, Fridge_id, products.Name
	) t
GROUP BY t.Fridge_id, t.Owner_name, t.FridgeName, t.NameOfModel
ORDER BY KindsOfProducts DESC) u)

SELECT DISTINCT products.Name, fridge.Owner_name, fridge.Name, fridge_model.NameOfModel 
FROM fridge_products, products, fridge, fridge_model 
WHERE Fridge_id = @FridgeId AND Product_id = products.id AND @FridgeId = fridge.id AND Model_id = fridge_model.id
GROUP BY products.Name, fridge.Owner_name, fridge.Name, fridge_model.NameOfModel