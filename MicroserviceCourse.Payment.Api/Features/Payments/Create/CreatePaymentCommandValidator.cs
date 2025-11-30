using FluentValidation;

namespace MicroserviceCourse.Payment.Api.Features.Payments.Create;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.OrderCode).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.CardNumber).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.CardExpirationDate).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.CardSecurityNumber).NotEmpty().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
    }
}
