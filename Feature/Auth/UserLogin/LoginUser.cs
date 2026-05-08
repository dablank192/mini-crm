using System;
using System.Security.Authentication;
using MediatR;
using mini_crm.Exception.AuthException;
using mini_crm.Infrastructure;
using mini_crm.Utils;

namespace mini_crm.Feature.Auth.UserLogin;

public record Command (
    string Username,
    string Password
) : IRequest<Result>;
public record Result (
    int UserId,
    string Token
);

public class LoginUser (
    AppDbContext dbContext,
    IUtil util
) : IRequestHandler<Command, Result>

{
    public static void MapEndpoint (RouteGroupBuilder group)
    {
        group.MapPost("/login", async(ISender sender, Command req)
        =>
        {
            var result = await sender.Send(req);

            return Results.Ok(result);
        })
        .WithName("User Login")
        .Produces<Result>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .AllowAnonymous();
    }

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var user = dbContext.User.FirstOrDefault(t => t.Username == req.Username)
        ?? throw new InvalidCredentialsException();
        
        var validPassword = util.PasswordVerify(req.Password, user!.Password);

        if (validPassword == false) throw new InvalidCredentialException();

        string accessToken = util.GenerateJwtToken(user);

        var response = new Result (
            UserId: user.Id,
            Token: accessToken
        );

        return response;
    }
}
