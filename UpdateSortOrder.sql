ALTER TABLE Photo ADD SortOrder int NULL;

UPDATE p
SET p.SortOrder = s.SortOrder
FROM Photo p
JOIN (
    SELECT 
		Id,
		ProductId,
		IsDefault,
		Data,
    DENSE_RANK() OVER (PARTITION BY ProductId ORDER BY Id, ProductId) AS SortOrder
FROM   
	Photo) s ON p.Id = s.Id;
	