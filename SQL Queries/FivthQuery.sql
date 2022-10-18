USE Fridges
SELECT Fridge_id,fridge.Name, fridge_model.NameOfModel, fridge.Owner_name, products.Name
FROM fridge_products, products, fridge, fridge_model
WHERE Product_id = products.id AND Fridge_id = fridge.id AND fridge.Model_id = fridge_model.id
AND fridge_products.Fridge_id = '4fb88ac7-616b-4763-aa44-76523d9a6051'