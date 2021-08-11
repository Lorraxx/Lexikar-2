DELETE FROM dbo.Translations
BULK
INSERT dbo.Translations
FROM 'C:\Users\Skaral\Desktop\Translations.csv'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n',
		 codepage = '65001',
		  KEEPNULLS
)
GO
--Check the content of the table.
SELECT *
FROM dbo.Translations
GO