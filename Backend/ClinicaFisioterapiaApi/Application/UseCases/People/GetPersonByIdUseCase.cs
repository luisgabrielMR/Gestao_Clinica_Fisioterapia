using AutoMapper;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.People;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Output;

namespace ClinicaFisioterapiaApi.Application.UseCases.People;

public class GetPersonByIdUseCase
{
    private readonly PersonRepository _repository;
    private readonly IMapper _mapper;

    public GetPersonByIdUseCase(PersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PersonDto?> ExecuteAsync(int id)
    {
        var person = await _repository.GetByIdAsync(id);
        return person == null ? null : _mapper.Map<PersonDto>(person);
    }
}
