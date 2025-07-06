using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;
using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Output;

namespace ClinicaFisioterapiaApi.Application.UseCases.Clinics;

public class GetClinicByIdUseCase
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IMapper _mapper;

    public GetClinicByIdUseCase(IClinicRepository clinicRepository, IMapper mapper)
    {
        _clinicRepository = clinicRepository;
        _mapper = mapper;
    }

    public async Task<ClinicDto?> ExecuteAsync(int id)
    {
        var clinic = await _clinicRepository.GetByIdAsync(id);
        return clinic == null ? null : _mapper.Map<ClinicDto>(clinic);
    }
}
