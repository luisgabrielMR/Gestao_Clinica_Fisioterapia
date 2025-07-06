using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;
using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Input;

namespace ClinicaFisioterapiaApi.Application.UseCases.Clinics;

public class UpdateClinicUseCase
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IMapper _mapper;

    public UpdateClinicUseCase(IClinicRepository clinicRepository, IMapper mapper)
    {
        _clinicRepository = clinicRepository;
        _mapper = mapper;
    }

    public async Task<bool> ExecuteAsync(int id, CreateClinicDto dto)
    {
        var existingClinic = await _clinicRepository.GetByIdAsync(id);
        if (existingClinic == null)
            return false;

        _mapper.Map(dto, existingClinic);
        await _clinicRepository.UpdateAsync(existingClinic);
        return true;
    }
}
