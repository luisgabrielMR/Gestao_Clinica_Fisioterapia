using AutoMapper;
using ClinicaFisioterapiaApi.Domain.Entities;
using ClinicaFisioterapiaApi.Infrastructure.Repositories.People;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Input;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace ClinicaFisioterapiaApi.Application.UseCases.People;

public class CreatePersonUseCase
{
    private readonly PersonRepository _repository;
    private readonly IMapper _mapper;

    public CreatePersonUseCase(PersonRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(CreatePersonDto dto)
    {
        var person = _mapper.Map<Person>(dto);
        person.CreatedAt = DateTime.UtcNow;
        person.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _repository.AddAsync(person);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
        {
            if (pgEx.ConstraintName == "people_cpf_key")
                throw new ApplicationException("Já existe uma pessoa cadastrada com este CPF.");
            else
                throw new ApplicationException("Violação de unicidade detectada.");
        }
    }
}
