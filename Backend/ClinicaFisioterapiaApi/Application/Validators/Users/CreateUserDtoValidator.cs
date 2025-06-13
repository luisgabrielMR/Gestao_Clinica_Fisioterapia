using ClinicaFisioterapiaApi.Interface.Dtos.Users;
using FluentValidation;

namespace ClinicaFisioterapiaApi.Application.Validators.Users
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O campo 'Username' é obrigatório.")
                .MaximumLength(100).WithMessage("O campo 'Username' deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo 'Password' é obrigatório.")
                .MinimumLength(6).WithMessage("O campo 'Password' deve ter no mínimo 6 caracteres.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("O campo 'Role' é obrigatório.")
                .Must(role => role == "admin" || role == "user")
                .WithMessage("O campo 'Role' deve ser 'admin' ou 'user'.");
        }
    }
}
