using System;
using Carter;
using mini_crm.Feature.Auth.UserLogin;
using mini_crm.Feature.Auth.UserRegister;

namespace mini_crm.Feature.Auth;

public class AuthApi : ICarterModule
{
    public void AddRoutes (IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/auth")
        .WithTags("Auth");


        RegisterUser.MapEndpoint(group);
        LoginUser.MapEndpoint(group);
    }
}
