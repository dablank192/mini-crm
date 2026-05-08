using System;
using FluentValidation;

namespace mini_crm.Feature.Auth.UserLogin;

public class DataValidation : AbstractValidator<Command>
{
    public void Configure()
    {
        RuleFor(t => t.Username)
        .MaximumLength(20).WithMessage("Username too long (username must has a maximum of 20)")
        .MinimumLength(5).WithMessage("Username too short (username must have a minimum of 5)")
        .NotEmpty().WithMessage("Username must not be empty");

        RuleFor(t => t.Password)
        .MinimumLength(8).WithMessage("Password too short (password must be >=8)")
        .MaximumLength(100).WithMessage("Password too long")
        .NotEmpty().WithMessage("Password must not be empty")
        .Matches(@"[\p{P}\p{S}]").WithMessage("Password must containt special character")
        .Matches(@"^[A-Z]").WithMessage("Password's first character must be upper case");
    }
}
