using System;
using Microsoft.AspNetCore.Identity;


namespace mini_crm.Utils;

public class Util : IUtil
{
    public string PasswordHasher (string plainPassword)
    {
        PasswordHasher<object> hasher = new();

        var hashedPassword = hasher.HashPassword( new object(), plainPassword);

        return hashedPassword;
    }

    public bool PasswordVerify (string plainPassword, string hashedPassword)
    {
        PasswordHasher<object> hasher = new();

        var result = hasher.VerifyHashedPassword(new object(), hashedPassword, plainPassword);

        if (result == PasswordVerificationResult.Success) return true;

        return false;
    }
}
