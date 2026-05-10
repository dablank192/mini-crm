using System;
using MediatR;
using mini_crm.Infrastructure;
using mini_crm.Model;
using mini_crm.Utils;

namespace mini_crm.Feature.Customer.AddVendor;

public class AddVendor (
    AppDbContext dbContext
) : IRequestHandler<Command, Result>

{
    public static void MapEndpoint (RouteGroupBuilder group)
    {
        group.MapPost("/{customerId}/vendor-detail", async(
            Guid customerId,
            ISender sender,
            SubCommand req
        ) =>
        {
            var result = await sender.Send(new Command(
                CustomerId: customerId,
                Name: req.Name,
                TaxIdentifierNumber: req.TaxIdentifierNumber,
                WebsiteUrl: req.WebsiteUrl,
                BankAccountNumber: req.BankAccountNumber,
                BankName: req.BankName
            ));
            return Results.Created();
        })
        .WithName("Add Vendor")
        .ProducesValidationProblem()
        .Produces<Result>(StatusCodes.Status201Created)
        .RequireAuthorization();
    }
    public record SubCommand (
        string Name,
        string TaxIdentifierNumber,
        string? WebsiteUrl,
        string BankAccountNumber,
        string BankName
    );

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var newVendor = new VendorDetail
        {
            CustomerId= req.CustomerId,
            CompanyName= req.Name,
            TaxIdentifierNumber= req.TaxIdentifierNumber,
            WebsiteUrl= req.WebsiteUrl,
            BankAccountNumber= req.BankAccountNumber,
            BankName= req.BankName
        };

        dbContext.VendorDetail.Add(newVendor);
        await dbContext.SaveChangesAsync(ct);

        return new Result();
    }
}
