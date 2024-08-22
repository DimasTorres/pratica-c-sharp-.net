namespace Pratica.Infra.Statements;

public static class ClientStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[Name]
              ,[PhoneNumber]
              ,[Email]
              ,[Address]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[Client]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Client]
           ([Id]
           ,[Name]
           ,[PhoneNumber]
           ,[Email]
           ,[Address]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES(
               @Id
               ,@Name
               ,@PhoneNumber
               ,@Email
               ,@Address
               ,@IsDeleted
               ,@CreatedAt)";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[Client]
           SET [Name] = @Name
              ,[PhoneNumber] = @PhoneNumber
              ,[Email] = @Email
              ,[Address] = @Address
         WHERE Id = @Id";

    public const string SQL_EXIST =
        @"SELECT 1 FROM [dbo].[Client] WHERE IsDeleted = 0 AND Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Client]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
