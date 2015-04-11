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

DECLARE cur_orderings CURSOR FOR
	SELECT 
		o.Id,
		o.Model
	FROM
		Ordering o

DECLARE @cmd NVARCHAR(150), 
		@fname NVARCHAR(50),
		@imagepath NVARCHAR(50),
		@formatfile NVARCHAR(50),
		@id INT,
		@model NVARCHAR(10);
		
SET @imagepath = 'C:\Images\Orderings\';
SET @formatfile = 'C:\Images\a.fmt'

OPEN cur_orderings

FETCH NEXT FROM cur_orderings INTO @id, @model

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @fname = @imagepath + @model + '_' + CONVERT(VARCHAR(5), @id) + '.jpg';
	SELECT @cmd = 'BCP "SELECT Photo FROM DEV1.Charltone.dbo.[Ordering] WHERE Id="' + CONVERT(VARCHAR(5), @id) + ' queryout ' + @fname + ' -T -S -f ' + @formatfile
	PRINT @cmd
	EXEC xp_cmdshell @cmd
	
	FETCH NEXT FROM cur_orderings INTO @id, @model
END

CLOSE cur_orderings;
DEALLOCATE cur_orderings;

-- SINGLE EXPORT
-- EXEC xp_cmdshell 'BCP "SELECT [Data] FROM DEV1.Charltone.dbo.[Photo] WHERE Id=334" queryout C:\Images\CL-RAM_334_default.jpg -T -S -f C:\Images\a.fmt';

-------------------------------------------------------------------------------
-- INSERT
-------------------------------------------------------------------------------

--BULK INSERT HomePageImage
--   FROM 'C:\Images\jpg\HomePageImage.jpg';
--GO