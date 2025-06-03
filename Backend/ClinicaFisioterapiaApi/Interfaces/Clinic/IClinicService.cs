using ClinicaFisioterapiaApi.Dtos.clinics;

namespace ClinicaFisioterapiaApi.Services
{
    public interface IClinicService
    {
        Task<IEnumerable<ClinicDto>> GetAllAsync();
        Task<ClinicDto?> GetByIdAsync(int id);
        Task<ClinicDto> CreateAsync(CreateClinicDto dto);
        Task<bool> UpdateAsync(int id, UpdateClinicDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
