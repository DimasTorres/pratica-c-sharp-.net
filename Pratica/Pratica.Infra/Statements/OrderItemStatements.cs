namespace Pratica.Infra.Statements;

public static class OrderItemStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[OrderId]
              ,[ProductId]
              ,[SellValue]
              ,[Quantity]
              ,[TotalAmout]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[OrderItem]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[OrderItem]
           ([Id]
           ,[OrderId]
           ,[ProductId]
           ,[SellValue]
           ,[Quantity]
           ,[TotalAmout]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
            (@Id
            ,@OrderId
            ,@ProductId
            ,@SellValue
            ,@Quantity
            ,@TotalAmout
            ,@IsDeleted
            ,@CreatedAt)";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[OrderItem]
           SET [OrderId] = @OrderId
              ,[ProductId] = @ProductId
              ,[SellValue] = @SellValue
              ,[Quantity] = @Quantity
              ,[TotalAmout] = @TotalAmout
         WHERE Id = @Id";

    public const string SQL_EXIST =
         @"SELECT 1 FROM OrderItem WHERE IsDeleted = 0 AND Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[OrderItem]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
