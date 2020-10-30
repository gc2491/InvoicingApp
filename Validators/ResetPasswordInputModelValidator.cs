using FatturaElettronica.Validators;
using FluentValidation;
using AspNET.Customizations.FatturaElettronicaNuGet.Tabelle;
using AspNET.Models.InputModels;

namespace AspNET.Validators
{
    public class ResetPasswordInputModelValidator : AbstractValidator<ResetPasswordInputModel>
    {
        public ResetPasswordInputModelValidator ()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo non può essere nullo")
                .Length(3, 15)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Matches("^[A-Za-z0-9]+$")
                .WithMessage("'{PropertyName}': il campo deve contenere maiuscole, minuscole e numeri")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Username")
                .OverridePropertyName("username");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithName("'{PropertyName}}: il campo non può essere nullo'")
                .Length(6, 20)
                .WithMessage("'{PropertyName}': il campo deve contenere tra {MinLength} e {MaxLength} caratteri. Caratteri ricevuti: {TotalLength}")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,20}$")
                .WithMessage("'{PropertyName}': il campo deve contenere lettere maiuscole, minuscole, numeri ed un carattere speciale")
                .Equal(x => x.ConfirmPassword)
                .WithMessage("'{PropertyName}': le password non combaciano")
                .Must(x => x == null ? true : !x.Contains("<") && !x.Contains(">"))
                .WithMessage("'{PropertyName}': il campo non può contere i caratteri '<' e '>'")
                .WithName("Password")
                .OverridePropertyName("password");
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("'{PropertyName}': il campo e obbligatorio")
                .WithName("Token")
                .OverridePropertyName("token");
        }
    }
}
