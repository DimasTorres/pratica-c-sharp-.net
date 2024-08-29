namespace Pratica.Infra.Statements;

public static class OrderStatements
{
    public const string SQL_BASE =
        @"SELECT o.Id
              ,o.IsDeleted
              ,o.CreatedAt
              ,c.Id
              ,c.Name
              ,c.PhoneNumber
              ,c.Email
              ,c.Address
              ,u.Id
              ,u.Name
              ,u.Email
          FROM [dbo].[Order] o
          INNER JOIN [dbo].[Client] c ON o.ClientId = c.Id
          INNER JOIN [dbo].[User] u ON o.UserId = u.id
          WHERE o.IsDeleted = 0 ";

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
         @"SELECT 1 FROM [dbo].[Order] WHERE IsDeleted = 0 AND Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Order]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
