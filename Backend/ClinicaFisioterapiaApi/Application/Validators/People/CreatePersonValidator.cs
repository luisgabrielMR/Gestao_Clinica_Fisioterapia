using ClinicaFisioterapiaApi.Common.Validators;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Input;
using FluentValidation;
namespace ClinicaFisioterapiaApi.Application.Validators.People;

public class CreatePersonValidator : AbstractValidator<CreatePersonDto>
{
    public CreatePersonValidator(ICpfValidator cpfValidator)
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("O nome completo é obrigatório.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(cpfValidator.IsValid).WithMessage("CPF inválido.");
    }
}
