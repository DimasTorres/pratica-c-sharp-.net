namespace Pratica.Infra.Statements;

public static class OrderStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[ClientId]
              ,[UserId]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[Order]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Order]
           ([Id]
           ,[ClientId]
           ,[UserId]
           ,[IsDeleted]
           ,[CreatedAt])
        VALUES
            (@Id
            ,@ClientId
            ,@UserId
            ,@IsDeleted
            ,@CreatedAt)";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[Order]
           SET [ClientId] = @ClientId
              ,[UserId] = @UserId
          WHERE Id = @Id";

    public const string SQL_EXIST =
         @"SELECT 1 FROM Order WHERE IsDeleted = 0 AND Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Order]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
