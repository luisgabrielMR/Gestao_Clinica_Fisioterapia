using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;
using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Output;

namespace ClinicaFisioterapiaApi.Application.UseCases.Clinics;

public class GetClinicsPagedUseCase
{
    private readonly IClinicRepository _clinicRepository;
    private readonly IMapper _mapper;

    public GetClinicsPagedUseCase(IClinicRepository clinicRepository, IMapper mapper)
    {
        _clinicRepository = clinicRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClinicDto>> ExecuteAsync(string? filter, string? sort, int page, int size)
    {
        var clinics = await _clinicRepository.GetPagedAsync(filter, sort, page, size);
        return _mapper.Map<IEnumerable<ClinicDto>>(clinics);
    }
}
