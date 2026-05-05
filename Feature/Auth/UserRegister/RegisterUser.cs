using System;
using System.Reflection.Metadata;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using mini_crm.Exception.AuthException;
using mini_crm.Infrastructure;
using mini_crm.Model;
using mini_crm.Utils;

namespace mini_crm.Feature.Auth.UserRegister;

public record Command (
    string Username,
    string Password
) : IRequest<Result>;
public record Result(
    int UserId,
    string Message
);

public class RegisterUser (
    AppDbContext dbContext,
    IUtil util
) : IRequestHandler<Command, Result>

{
    public static void MapEndpoint (RouteGroupBuilder group)
    {
        group.MapPost("/register", async (ISender sender, Command req)
        =>
        {
            var result = await sender.Send(req);

            return Results.Ok(result);
        })
        .WithName("User Register")
        .AllowAnonymous()
        .Produces<Result>(StatusCodes.Status200OK)
        .ProducesValidationProblem();
    }

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var validUsername = dbContext.User.FirstOrDefault(t => t.Username == req.Username);

        if (validUsername != null) throw new DuplicateUsernameException(req.Username);
        
        var hashedPassword = util.PasswordHasher(req.Password);

        var newUser = new User
        {
            Username= req.Username,
            Password= hashedPassword
        };

        dbContext.User.Add(newUser);

        await dbContext.SaveChangesAsync(ct);

        var result = new Result(
            UserId: newUser.Id,
            Message: "User created successfully"
        );

        return result;
    }
}
