using System;
using Carter;
using mini_crm.Feature.Customer.AddClientDetail;
using mini_crm.Feature.Customer.AddCustomer;
using mini_crm.Feature.Customer.AddPartner;
using mini_crm.Feature.Customer.GetCustomerInfo;
using mini_crm.Feature.Customer.ListAllCustomer;
using mini_crm.Feature.Customer.UpdateCustomer;
using mini_crm.Model;

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
        PartnerAdd.MapEndpoint(group);

        CustomerInfo.MapEndpoint(group);
        CustomerUpdate.MapEndpoint(group);
    }
}
