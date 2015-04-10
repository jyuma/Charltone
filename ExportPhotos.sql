Use Charltone
/*
EXEC sp_configure 'show advanced options', 1
GO
RECONFIGURE
GO
EXEC sp_configure 'xp_cmdshell', 1
GO
RECONFIGURE
GO
*/

----------------------------------------------------------------------------------
-- EXTRACT
----------------------------------------------------------------------------------

DECLARE cur_photos CURSOR FOR
	SELECT 
		ph.Id,
		i.Model,
		ph.IsDefault
	FROM
		Product p
		INNER JOIN Instrument i ON p.InstrumentId = i.Id
		INNER JOIN Photo ph ON p.Id = ph.ProductId;

DECLARE @cmd NVARCHAR(150), 
		@fname NVARCHAR(50),
		@imagepath NVARCHAR(50),
		@formatfile NVARCHAR(50),
		@id INT,
		@model NVARCHAR(10),
		@default BIT;
		
SET @imagepath = 'C:\Images\jpg\';
SET @formatfile = 'C:\Images\a.fmt'

OPEN cur_photos

FETCH NEXT FROM cur_photos INTO @id, @model, @default

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @fname = @imagepath + @model + '_' + CONVERT(VARCHAR(5), @id) + CASE WHEN @default = 1 THEN '_default' ELSE '' END + '.jpg';
	SELECT @cmd = 'BCP "SELECT [Data] FROM DEV1.Charltone.dbo.[Photo] WHERE Id="' + CONVERT(VARCHAR(5), @id) + ' queryout ' + @fname + ' -T -S -f ' + @formatfile
	PRINT @cmd
	EXEC xp_cmdshell @cmd
	
	FETCH NEXT FROM cur_photos INTO @id, @model, @default
END

CLOSE cur_photos;
DEALLOCATE cur_photos;

-------------------------------------------------------------------------------
-- INSERT
-------------------------------------------------------------------------------

--BULK INSERT HomePageImage
--   FROM 'C:\Images\jpg\HomePageImage.jpg';
--GO