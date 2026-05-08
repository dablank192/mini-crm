using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mini_crm.Dto;
using mini_crm.Infrastructure;

namespace mini_crm.Feature.Customer.ListAllCustomer;

public record Query(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<Result>;
public record Result (
    List<CustomerDto> Data,
    int TotalCount,
    int CurrentPage,
    int TotalPage

);

public class CustomerList (
    AppDbContext dbContext
) : IRequestHandler<Query, Result>

{
    public static void MapEndpoint (RouteGroupBuilder group)
    {
        group.MapGet("/", async (ISender sender, [AsParameters]Query req)
        =>
        {
            var result = await sender.Send(req);

            return Results.Ok(result);
        })
        .WithName("Get All Customer")
        .Produces<Result>(StatusCodes.Status200OK)
        .ProducesValidationProblem()
        .RequireAuthorization();
    }

    public async Task<Result> Handle (Query req, CancellationToken ct)
    {
        var totalCount = await dbContext.Customer.CountAsync(ct);

        var skip = (req.PageNumber - 1) * req.PageSize;

        var customer = await dbContext.Customer
        .AsNoTracking()
        .OrderByDescending(t => t.FirstName)
        .Skip(skip)
        .Take(req.PageSize)
        .Select(t => new CustomerDto
        {
            FirstName= t.FirstName,
            LastName= t.LastName,
            Email= t.Email,
            PhoneNumber= t.PhoneNumber,
            CustomerTag= t.CustomerTag
        })
        .ToListAsync(ct);

        var totalPage = totalCount/req.PageSize;

        var response = new Result(
            Data: customer,
            TotalCount: totalCount,
            CurrentPage: req.PageNumber,
            TotalPage: totalPage
        );

        return response;
    }
}
