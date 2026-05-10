using System;
using Carter;
using mini_crm.Feature.Customer.AddClientDetail;
using mini_crm.Feature.Customer.AddCustomer;
using mini_crm.Feature.Customer.ListAllCustomer;

namespace mini_crm.Feature.Customer;

public class CustomerApi : ICarterModule
{
    public void AddRoutes (IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/customer")
        .WithName("Customer Management");


        CustomerAdd.MapEndpoint(group);
        CustomerList.MapEndpoint(group);

        ClientDetailAdd.MapEndpoint(group);
        AddVendor.AddVendor.MapEndpoint(group);
    }
}
