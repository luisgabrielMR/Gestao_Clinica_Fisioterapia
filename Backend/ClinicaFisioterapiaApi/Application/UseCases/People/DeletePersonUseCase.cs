using ClinicaFisioterapiaApi.Infrastructure.Repositories.People;

namespace ClinicaFisioterapiaApi.Application.UseCases.People;

public class DeletePersonUseCase
{
    private readonly PersonRepository _repository;

    public DeletePersonUseCase(PersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        var person = await _repository.GetByIdAsync(id);
        if (person == null) return false;

        await _repository.DeleteAsync(person);
        return true;
    }
}
