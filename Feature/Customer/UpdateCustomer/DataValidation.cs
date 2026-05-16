using System;
using FluentValidation;
using MediatR;

namespace mini_crm.Feature.Customer.UpdateCustomer;

public record Command(
    Guid CustomerId,
    SubCommand Payload
) : IRequest<Result>;

public record SubCommand (
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber
    );
public record Result();

public class DataValidation : AbstractValidator<SubCommand>
{
    public DataValidation()
    {
        RuleFor(t => t.FirstName)
        .NotEmpty().WithMessage("Customer's name must not be empty")
        .MaximumLength(100).WithMessage("Customer name is too long");

        RuleFor(t => t.LastName)
        .NotEmpty().WithMessage("Customer's name must not be empty")
        .MaximumLength(100).WithMessage("Customer name is too long");

        When(t => !string.IsNullOrWhiteSpace(t.Email), () =>
        {
            RuleFor(t => t.Email)
            .EmailAddress().WithMessage("Invalid Email Format")
            .MaximumLength(200).WithMessage("Email too long");  
        });

        When(t => !string.IsNullOrWhiteSpace(t.PhoneNumber), () =>
        {
            RuleFor(t => t.PhoneNumber)
            .MinimumLength(8).WithMessage("Phone number must be 8-digit")
            .MaximumLength(8).WithMessage("Phone number must be 8-digit");
        });
    }
}
