using FluentValidation;

namespace Application.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(6).WithMessage("Hasło musi mieć przynajmniej 6 znaków")
                .Matches("[A-Z]").WithMessage("Hasło musi mieć przynajmniej 1 wielką literę")
                .Matches("[a-z]").WithMessage("Hasło musi mieć przynajmniej 1 małą literę")
                .Matches("[0-9]").WithMessage("Hasło musi mieć przynajmniej jedną cyfrę");

            return options;
        }
    }
}