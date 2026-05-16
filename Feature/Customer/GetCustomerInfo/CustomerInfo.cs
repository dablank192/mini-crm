using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mini_crm.Dto;
using mini_crm.Exception.Customer;
using mini_crm.Infrastructure;

namespace mini_crm.Feature.Customer.GetCustomerInfo;

public class CustomerInfo(
    AppDbContext dbContext
) : IRequestHandler<Command, Result>

{
    public static void MapEndpoint(RouteGroupBuilder group)
    {
        group.MapGet("/{customerId}/customer-info", async(
            ISender sender,
            Guid customerId
        ) =>
        {
            var result = await sender.Send(new Command(customerId));
            return Results.Ok(result);
        })
        .WithName("Get customer info")
        .ProducesValidationProblem()
        .Produces<Result>(StatusCodes.Status200OK)
        .RequireAuthorization();
    }

    public async Task<Result> Handle (Command req, CancellationToken ct)
    {
        var customer = await dbContext.Customer.
        AsNoTracking().
        Where(t => t.Id == req.CustomerId).
        Select(t => new
        {
            Type= t.CustomerTag,
            Base= t,
            ClientDetail= t.ClientDetail,
            VendorDetail= t.VendorDetail,
            PartnerDetail= t.PartnerDetail
        })
        .FirstOrDefaultAsync(ct);

        if(customer == null)
        {
            throw new CustomerIdNotExistedException(req.CustomerId);
        }

        CustomerInfoResponseDto response = customer.Type switch
        {
            CustomerCategory.Client => new ClientInfoDto
            {
                CustomerId= customer.Base.Id,
                ClientType= customer.ClientDetail!.Type,
                CompanyName= customer.ClientDetail.CompanyName,
                DateOfBirth= customer.ClientDetail.DateOfBirth,
                TotalSpent= customer.ClientDetail.TotalSpent,
                LatestPurchaseDate= customer.ClientDetail.LatestPurchaseDate
            },

            CustomerCategory.Partner => new PartnerInfoDto
            {
                CustomerId= customer.Base.Id,
                CompanyName= customer.PartnerDetail?.CompanyName,
                TaxIdentifierNumber= customer.PartnerDetail?.TaxIdentifierNumber,
                ContractNumber= customer.PartnerDetail?.ContractNumber,
                ContractEffectiveDate= customer.PartnerDetail?.ContractEffectiveDate ?? default,
                ContractExpiryDate= customer.PartnerDetail?.ContractExpiryDate ?? default,
                TotalRevenue= customer.PartnerDetail?.TotalRevenue ?? 0
            },

            CustomerCategory.Vendor => new VendorInfoDto
            {
                CustomerId= customer.Base.Id,
                CompanyName= customer.VendorDetail?.CompanyName,
                TaxIdentifierNumber= customer.VendorDetail?.TaxIdentifierNumber,
                WebsiteUrl= customer.VendorDetail?.WebsiteUrl,
                BankAccountNumber= customer.VendorDetail?.BankAccountNumber,
                BankName= customer.VendorDetail?.BankName,
                CurrentDebt= customer.VendorDetail?.CurrentDebt
            },

            _ => throw new InvalidCustomerTagException(customer.Type)
        };

        return new Result(Data: response);
    }
}
