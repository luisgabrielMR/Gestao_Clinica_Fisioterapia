using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Input;
using FluentValidation;

namespace ClinicaFisioterapiaApi.Application.Validators.Clinics;

public class CreateClinicValidator : AbstractValidator<CreateClinicDto>
{
    public CreateClinicValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O nome da clínica é obrigatório.")
            .MaximumLength(255).WithMessage("O nome da clínica deve ter no máximo 255 caracteres.");

        RuleFor(c => c.Address)
            .NotEmpty().WithMessage("O endereço é obrigatório.");

        RuleFor(c => c.City)
            .NotEmpty().WithMessage("A cidade é obrigatória.");

        RuleFor(c => c.ZipCode)
            .MaximumLength(10).WithMessage("O CEP deve ter no máximo 10 caracteres.");
    }
}
