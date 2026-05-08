using System;
using Carter;
using mini_crm.Feature.Customer.AddCustomer;

namespace mini_crm.Feature.Customer;

public class CustomerApi : ICarterModule
{
    public void AddRoutes (IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/customer")
        .WithName("Customer Management");


        CustomerAdd.MapEndpoint(group);
    }
}
