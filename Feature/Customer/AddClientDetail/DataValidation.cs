using System;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using mini_crm.Dto;


namespace mini_crm.Feature.Customer.AddClientDetail;

public record Command(
    Guid CustomerId,
    ClientType Type,
    string CompanyName, //if client type is Corporate, else null
    DateOnly? DateOfBirth
    ) : IRequest<Result>;
public record Result(
);

public class DataValidation : AbstractValidator<Command>
{
    public DataValidation()
    {
        When(t => !string.IsNullOrEmpty(t.CompanyName), () =>
        {
            RuleFor(t => t.CompanyName)
            .MaximumLength(50);
        });
    }
}
