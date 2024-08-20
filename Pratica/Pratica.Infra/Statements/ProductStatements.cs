namespace Pratica.Infra.Statements;

public static class ProductStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[Description]
              ,[SellValue]
              ,[Stock]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[Product]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Product]
           ([Id]
           ,[Description]
           ,[SellValue]
           ,[Stock]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
            (@Id
            ,@Description
            ,@SellValue
            ,@Stock
            ,@IsDeleted
            ,@CreatedAt)";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[Product]
           SET [Description] = @Description
              ,[SellValue] = @SellValue
              ,[Stock] = @Stock
             WHERE Id = @Id";

    public const string SQL_EXIST =
         @"SELECT 1 FROM Product WHERE IsDeleted = 0 AND Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Product]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
