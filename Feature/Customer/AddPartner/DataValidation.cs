using System;
using System.Data;
using FluentValidation;
using MediatR;

namespace mini_crm.Feature.Customer.AddPartner;

public record Command(
    Guid CustomerId,
    string CompanyName,
    string TaxIdentifierNumber,
    string ContractNumber,
    DateOnly ContractEffectiveDate,
    DateOnly ContractExpiryDate
    ) : IRequest<Result>;

public record Result ();

public class DataValidation : AbstractValidator<Command>
{
    public DataValidation()
    {
        RuleFor(t => t.CompanyName)
        .MaximumLength(100).WithMessage("Exceed maximum lengthj")
        .NotEmpty().WithMessage("Company name must no be empty");

        RuleFor(t => t.TaxIdentifierNumber)
        .MinimumLength(10).WithMessage("Tax Identifier required 10-13 digits")
        .MaximumLength(13).WithMessage("Tax Identifier required 10-13 digits")
        .NotEmpty().WithMessage("Tax Indentifier must not be empty");

        RuleFor(t => t.ContractEffectiveDate)
        .LessThan(t => t.ContractExpiryDate)
        .WithMessage("Contract effective date must be less than expiry date ");

        RuleFor(t => t.ContractNumber)
        .MinimumLength(6).WithMessage("Contract number must from 6-20 digits")
        .MaximumLength(20).WithMessage("Contract number must from 6-20 digits");
    }
}
