USE Fridges
SELECT TOP 1 w.Year, s.Name, w.NameOfModel, s.Owner_name, s.NumberOfProducts AS NumberOfProducts 
FROM 
     (SELECT Fridge_id, fridge.Name, fridge.Owner_name, fridge.Model_id, Sum(Quantity)AS NumberOfProducts
      FROM fridge_products
	  JOIN fridge ON fridge_products.Fridge_id = fridge.id 
      GROUP BY Fridge_id, fridge.Name, fridge.Owner_name ,fridge.Model_id
	  ) s 
JOIN fridge_model w ON s.Model_id = w.id
GROUP BY s.Name, w.NameOfModel, s.Owner_name, w.Year, s.Fridge_id, s.NumberOfProducts
ORDER BY s.NumberOfProducts DESC