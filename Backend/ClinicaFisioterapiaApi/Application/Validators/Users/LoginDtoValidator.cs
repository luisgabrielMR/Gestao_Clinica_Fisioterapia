using ClinicaFisioterapiaApi.Interface.Dtos.Users;
using FluentValidation;

namespace ClinicaFisioterapiaApi.Application.Validators.Users
{
    public class LoginDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .MaximumLength(100).WithMessage("O nome de usuário deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
        }
    }
}
