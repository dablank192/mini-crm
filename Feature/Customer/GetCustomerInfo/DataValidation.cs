using System;
using MediatR;
using mini_crm.Dto;

namespace mini_crm.Feature.Customer.GetCustomerInfo;

public record Command(Guid CustomerId) : IRequest<Result>;
public record Result(CustomerInfoResponseDto Data);

public class DataValidation
{

}
