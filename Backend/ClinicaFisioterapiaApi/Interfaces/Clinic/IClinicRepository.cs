using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaFisioterapiaApi.Models;

namespace ClinicaFisioterapiaApi.Interfaces.Clinic
{
    public interface IClinicRepository
    {
        Task<IEnumerable<ClinicaFisioterapiaApi.Models.Clinic>> GetAllAsync();
        Task<ClinicaFisioterapiaApi.Models.Clinic?> GetByIdAsync(int id);
        Task AddAsync(ClinicaFisioterapiaApi.Models.Clinic clinic);
        Task UpdateAsync(ClinicaFisioterapiaApi.Models.Clinic clinic);
        Task DeleteAsync(ClinicaFisioterapiaApi.Models.Clinic clinic);
        Task<bool> SaveChangesAsync();
    }
}
