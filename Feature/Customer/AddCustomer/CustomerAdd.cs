using System;
using MediatR;
using mini_crm.Exception.Customer;
using mini_crm.Infrastructure;
using mini_crm.Utils;

namespace mini_crm.Feature.Customer.AddCustomer;

public class CustomerAdd(
    AppDbContext dbContext,
    IUtil util
) : IRequestHandler<Command, Result>
{
    public static void MapEndpoint (RouteGroupBuilder group)
    {
        group.MapPost("/new-customer", async(ISender sender, Command req)
        =>
        {
            await sender.Send(req);
            return Results.Created();
        })
        .WithName("Add Customer")
        .Produces<Result>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .RequireAuthorization();
    }

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var userId = util.GetUserId();
        
        var duplicatePhoneNum = dbContext.Customer.FirstOrDefault(t => t.PhoneNumber == req.PhoneNumber);

        if (duplicatePhoneNum != null) throw new DuplicateCustomerException();
        
        var newCustomer = new Model.Customer
        {
            UserId= userId,
            FirstName= req.FirstName,
            LastName= req.LastName,
            Email= req.Email,
            PhoneNumber= req.PhoneNumber
        };

        if (req.CustomerTag.HasValue)
        {
            newCustomer.CustomerTag = req.CustomerTag.Value;
        }

        dbContext.Customer.Add(newCustomer);
        await dbContext.SaveChangesAsync(ct);

        var response = new Result();

        return response;
    }
}
