﻿namespace Pratica.Application.DataContract.User.Response;

public sealed class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
}
