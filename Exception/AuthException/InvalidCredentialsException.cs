using System;

namespace mini_crm.Exception.AuthException;

public class InvalidCredentialsException : System.Exception
{
    public InvalidCredentialsException () : base(
        $"Username or password invalid"
    ) {}
}
