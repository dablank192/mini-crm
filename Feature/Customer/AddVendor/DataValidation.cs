using System;
using System.Text.RegularExpressions;
using FluentValidation;
using MediatR;

namespace mini_crm.Feature.Customer.AddVendor;

public record Command (
    Guid CustomerId,
    string Name,
    string TaxIdentifierNumber,
    string? WebsiteUrl,
    string BankAccountNumber,
    string BankName
) : IRequest<Result>;
public record Result();

public class DataValidation : AbstractValidator<Command>
{
    public DataValidation ()
    {
        RuleFor(t => t.TaxIdentifierNumber)
        .MinimumLength(10).WithMessage("Tax Identifier required 10-13 digits")
        .MaximumLength(13).WithMessage("Tax Identifier required 10-13 digits")
        .NotEmpty().WithMessage("Tax Indentifier must not be empty");

        RuleFor(t => t.WebsiteUrl)
        .Must(t => Regex.IsMatch(t, @"(\.com|\.net|\.vn)$", RegexOptions.IgnoreCase)).WithMessage("Invalid domain")
        .MaximumLength(50).WithMessage("Url is too long")
        .NotEmpty().WithMessage("Company website must not be empty");

        RuleFor(t => t.BankAccountNumber)
        .MinimumLength(8).WithMessage("Account number required 8-15 digits")
        .MaximumLength(15).WithMessage("Account number required 8-15 digits");
    }
}
