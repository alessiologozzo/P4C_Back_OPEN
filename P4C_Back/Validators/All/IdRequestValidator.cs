using FluentValidation;
using P4C_Back.Requests.All;

namespace P4C_Back.Validators.All
{
    public class IdRequestValidator : AbstractValidator<IdRequest>
    {
        public IdRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).NotEmpty().GreaterThanOrEqualTo(1);
        }
    }
}
