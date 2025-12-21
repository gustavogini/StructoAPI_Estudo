using FluentValidation;
using Structo.Communication.Requests;
using Structo.Exceptions;

namespace Structo.Application.UseCases.Company
{
    public class CompanyValidator : AbstractValidator<RequestCompanyJson>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage(ResourceMessagesException.COMPANY_NAME_EMPTY)
                .MaximumLength(255).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);//todos que estao com unknown error, trocar depois
            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage(ResourceMessagesException.CNPJ_EMPTY)
                .MaximumLength(255).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY)
                .MaximumLength(255).WithMessage(ResourceMessagesException.UNKNOWN_ERROR)
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(ResourceMessagesException.PHONE_EMPTY)
                .MaximumLength(50).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(ResourceMessagesException.ADDRESS_EMPTY)
                .MaximumLength(255).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.Number)
                .GreaterThan(0).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.District)
                .NotEmpty().WithMessage(ResourceMessagesException.DISTRICT_EMPTY)
                .MaximumLength(255).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.City)
                .NotEmpty().WithMessage(ResourceMessagesException.CITY_EMPTY)
                .MaximumLength(255).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.State)
                .NotEmpty().WithMessage(ResourceMessagesException.STATE_EMPTY)
                .MaximumLength(50).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage(ResourceMessagesException.ZIPCODE_EMPTY)
                .MaximumLength(50).WithMessage(ResourceMessagesException.UNKNOWN_ERROR);
        }
    }
}
