using AutoMapper;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.People;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Output;

namespace ClinicaFisioterapiaApi.Application.UseCases.People;

public class GetAllPeopleUseCase
{
    private readonly PersonRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPeopleUseCase(PersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<PersonDto>> ExecuteAsync()
    {
        var people = await _repository.GetAllAsync();
        return _mapper.Map<List<PersonDto>>(people);
    }
}
