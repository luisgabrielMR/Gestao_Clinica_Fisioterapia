using AutoMapper;
using ClinicaFisioterapiaApi.Domain.Entities;
using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;
using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Input;

namespace ClinicaFisioterapiaApi.Application.UseCases.Clinics;

public class CreateClinicUseCase
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IMapper _mapper;

    public CreateClinicUseCase(IClinicRepository clinicRepository, IMapper mapper)
    {
        _clinicRepository = clinicRepository;
        _mapper = mapper;
    }

    public async Task<int> ExecuteAsync(CreateClinicDto dto)
    {
        var clinic = _mapper.Map<Clinic>(dto);
        await _clinicRepository.CreateAsync(clinic);
        return clinic.ClinicId;
    }
}
