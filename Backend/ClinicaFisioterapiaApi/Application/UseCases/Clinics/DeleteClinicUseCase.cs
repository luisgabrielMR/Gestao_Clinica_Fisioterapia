using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;

namespace ClinicaFisioterapiaApi.Application.UseCases.Clinics;

public class DeleteClinicUseCase
{
    private readonly IClinicRepository _clinicRepository;

    public DeleteClinicUseCase(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        var clinic = await _clinicRepository.GetByIdAsync(id);
        if (clinic == null)
            return false;

        await _clinicRepository.SoftDeleteAsync(id);
        return true;
    }
}
