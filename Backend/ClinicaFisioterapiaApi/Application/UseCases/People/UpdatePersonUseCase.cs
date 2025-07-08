using AutoMapper;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.People;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Input;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ClinicaFisioterapiaApi.Application.UseCases.People;

public class UpdatePersonUseCase
{
    private readonly PersonRepository _repository;
    private readonly IMapper _mapper;

    public UpdatePersonUseCase(PersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> ExecuteAsync(int id, CreatePersonDto dto)
{
    var person = await _repository.GetByIdAsync(id);
    if (person == null) return false;

    _mapper.Map(dto, person);
    person.CreatedAt = DateTime.SpecifyKind(person.CreatedAt, DateTimeKind.Utc);
    person.UpdatedAt = DateTime.UtcNow;

    try
    {
        await _repository.UpdateAsync(person);
        return true;
    }
    catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
    {   
        if (pgEx.ConstraintName == "people_cpf_key")
            throw new ApplicationException("Já existe outra pessoa cadastrada com este CPF.");
        else
            throw new ApplicationException("Violação de unicidade detectada.");
    }
}

}
