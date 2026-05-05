using System;

namespace mini_crm.Exception.AuthException;

public class DuplicateUsernameException : System.Exception
{
    public DuplicateUsernameException(string username) : base(
        $"Username '{username}' is already taken, please try again"
    ) {}
}
