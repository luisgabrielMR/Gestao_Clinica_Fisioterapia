using ClinicaFisioterapiaApi.Interface.Dtos.Users.Input;

namespace ClinicaFisioterapiaApi.Application.Validators.Users
{
    using FluentValidation;

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O campo 'Username' é obrigatório.")
                .MaximumLength(100).WithMessage("O campo 'Username' deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo 'Password' é obrigatório.")
                .MinimumLength(6).WithMessage("O campo 'Password' deve ter no mínimo 6 caracteres.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("O campo 'Role' deve ser um valor válido: Admin, Fisioterapeuta ou Atendente.");
        }
    }
}
