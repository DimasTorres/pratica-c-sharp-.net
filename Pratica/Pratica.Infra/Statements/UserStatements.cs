namespace Pratica.Infra.Statements;

public static class UserStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[Name]
              ,[Login]
              ,[PasswordHash]
              ,[Email]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[User]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[User]
           ([Id]
           ,[Name]
           ,[Login]
           ,[PasswordHash]
           ,[Email]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
           (@Id
           ,@Name
           ,@Login
           ,@PasswordHash
           ,@Email
           ,@IsDeleted
           ,@CreatedAt)";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[User]
           SET [Name] = @Name
              ,[Login] = @Login
              ,[PasswordHash] = @PasswordHash
              ,[Email] = @Email
            WHERE Id = @Id";

    public const string SQL_EXIST =
         @"SELECT 1 FROM User WHERE IsDeleted = 0 AND Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[User]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
