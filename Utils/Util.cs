using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using mini_crm.Model;


namespace mini_crm.Utils;

public class Util : IUtil
{
    private readonly IConfiguration _config;

    public Util (IConfiguration config)
    {
        _config = config;
    }

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

    public string GenerateJwtToken(User user)
    {
        var jwt = _config.GetSection("Jwt");

        var claim = new List<Claim>
        {
            new Claim("Username", user.Username.ToString()),
            new Claim("UserId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claim,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}
