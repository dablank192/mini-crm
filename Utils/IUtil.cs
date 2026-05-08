using System;
using mini_crm.Model;


namespace mini_crm.Utils;

public interface IUtil
{
    public string PasswordHasher (string plainPassword);
    public bool PasswordVerify (string plainPassword, string hashedPassword);
    public string GenerateJwtToken(User user);
}
