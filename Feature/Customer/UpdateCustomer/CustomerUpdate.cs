using System;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mini_crm.Exception.Customer;
using mini_crm.Infrastructure;

namespace mini_crm.Feature.Customer.UpdateCustomer;

public class CustomerUpdate(
    AppDbContext dbContext
) : IRequestHandler<Command, Result>

{
    public static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapPatch("/{customerId}/customer-update", async(
            ISender sender,
            Guid customerId,
            SubCommand payload
        ) =>
        {
            var result = await sender.Send(new Command(customerId, payload));

            return Results.Ok(result);
        })
        .WithName("Update customer basic info")
        .ProducesValidationProblem()
        .Produces<Result>(StatusCodes.Status200OK);
    }

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var customer = await dbContext.Customer.FirstOrDefaultAsync(t => t.Id == req.CustomerId, ct)
        ?? throw new CustomerIdNotExistedException(req.CustomerId);

        var mapConfig = new TypeAdapterConfig();
        mapConfig.NewConfig<SubCommand, Model.Customer>()
        .IgnoreNullValues(true);

        customer.UpdatedAt = DateTime.UtcNow;

        req.Payload.Adapt(customer, mapConfig);

        await dbContext.SaveChangesAsync(ct);

        return new Result();
    }
}
