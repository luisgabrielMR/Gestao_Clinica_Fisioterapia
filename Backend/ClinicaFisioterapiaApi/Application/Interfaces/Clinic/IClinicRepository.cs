using ClinicaFisioterapiaApi.Domain.Entities;

namespace ClinicaFisioterapiaApi.Application.Interfaces.Repositories;

public interface IClinicRepository
{
    Task CreateAsync(Clinic clinic);
    Task<Clinic?> GetByIdAsync(int id);
    Task<IEnumerable<Clinic>> GetAllNotDeletedAsync();
    Task<IEnumerable<Clinic>> GetPagedAsync(string? filter, string? sort, int page, int size);
    Task UpdateAsync(Clinic clinic);
    Task SoftDeleteAsync(int id);
}
