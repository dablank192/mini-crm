using System;
using MediatR;
using mini_crm.Infrastructure;
using mini_crm.Model;

namespace mini_crm.Feature.Customer.AddPartner;

public class PartnerAdd(
    AppDbContext dbContext
) : IRequestHandler<Command, Result>
{
    public static void MapEndpoint (RouteGroupBuilder group)
    {
        group.MapPost("/{customerId}/partner-detail", async(
            ISender sender,
            Guid customerId,
            SubCommand req
        ) =>
        {
            var command = new Command(
                CustomerId: customerId,
                CompanyName: req.CompanyName,
                TaxIdentifierNumber: req.TaxIdentifierNumber,
                ContractNumber: req.ContractNumber,
                ContractEffectiveDate: req.ContractEffectiveDate,
                ContractExpiryDate: req.ContractExpiryDate
            );

            var result = await sender.Send(command);

            return Results.Created();
        })
        .WithName("Add Partner")
        .Produces<Result>(StatusCodes.Status201Created)
        .ProducesValidationProblem();
    }
    public record SubCommand(
    string CompanyName,
    string TaxIdentifierNumber,
    string ContractNumber,
    DateOnly ContractEffectiveDate,
    DateOnly ContractExpiryDate
    );

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var newPartner = new PartnerDetail
        {
            CustomerId= req.CustomerId,
            CompanyName= req.CompanyName,
            TaxIdentifierNumber= req.TaxIdentifierNumber,
            ContractNumber= req.ContractNumber,
            ContractEffectiveDate= req.ContractEffectiveDate,
            ContractExpiryDate= req.ContractExpiryDate
        };

        dbContext.PartnerDetail.Add(newPartner);
        await dbContext.SaveChangesAsync(ct);

        return new Result();
    }
}
