using System;
using mini_crm.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using mini_crm.Infrastructure;
using mini_crm.Exception.Customer;
using mini_crm.Model;
using Microsoft.EntityFrameworkCore;

namespace mini_crm.Feature.Customer.AddClientDetail;

public class ClientDetailAdd(
    AppDbContext dbContext
) : IRequestHandler<Command, Result>

{  
    public static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPost("/{customerId}/client-detail", async(
            ISender sender,
            Guid customerId,
            [FromBody]SubCommand req
        ) =>
        {
            var command = new Command(
                CustomerId: customerId,
                Type: req.Type,
                CompanyName: req.CompanyName,
                DateOfBirth: req.DateOfBirth
            );

            await sender.Send(command);

            return Results.Created();
        })
        .WithName("Add Client Details")
        .Produces<Result>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .RequireAuthorization();
    }
    public record SubCommand (
        ClientType Type,
        string CompanyName, //if client type is Corporate, else null
        DateOnly? DateOfBirth
    );

    public async Task<Result> Handle(Command req, CancellationToken ct)
    {
        var customer = await dbContext.Customer.FirstOrDefaultAsync(t => t.Id == req.CustomerId, ct)
        ?? throw new CustomerIdNotExistedException(req.CustomerId);

        var clientDetail = new ClientDetail
        {
            CustomerId= req.CustomerId,
            Type= req.Type,
            DateOfBirth= req.DateOfBirth
        };

        if(req.Type == ClientType.Corporate)
        {
            clientDetail.CompanyName = req.CompanyName;
        }

        dbContext.ClientDetail.Add(clientDetail);

        await dbContext.SaveChangesAsync(ct);

        return new Result();
    }
}
