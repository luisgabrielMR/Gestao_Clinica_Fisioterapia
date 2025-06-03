using ClinicaFisioterapiaApi.Dtos.clinics;
using ClinicaFisioterapiaApi.Interfaces.Clinic;
using ClinicaFisioterapiaApi.Models;

namespace ClinicaFisioterapiaApi.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _repository;

        public ClinicService(IClinicRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClinicDto>> GetAllAsync()
        {
            var clinics = await _repository.GetAllAsync();
            return clinics.Select(clinic => new ClinicDto
            {
                ClinicId = clinic.ClinicId,
                Name = clinic.Name,
                Address = clinic.Address,
                Neighborhood = clinic.Neighborhood,
                City = clinic.City,
                State = clinic.State,
                Zipcode = clinic.Zipcode,
                CreatedAt = clinic.CreatedAt,
                UpdatedAt = clinic.UpdatedAt
            });
        }

        public async Task<ClinicDto?> GetByIdAsync(int id)
        {
            var clinic = await _repository.GetByIdAsync(id);
            if (clinic is null)
                return null;

            return new ClinicDto
            {
                ClinicId = clinic.ClinicId,
                Name = clinic.Name,
                Address = clinic.Address,
                Neighborhood = clinic.Neighborhood,
                City = clinic.City,
                State = clinic.State,
                Zipcode = clinic.Zipcode,
                CreatedAt = clinic.CreatedAt,
                UpdatedAt = clinic.UpdatedAt
            };
        }

        public async Task<ClinicDto> CreateAsync(CreateClinicDto dto)
        {
            var clinic = new Clinic
            {
                Name = dto.Name,
                Address = dto.Address,
                Neighborhood = dto.Neighborhood,
                City = dto.City,
                State = dto.State,
                Zipcode = dto.Zipcode,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(clinic);

            return new ClinicDto
            {
                ClinicId = clinic.ClinicId,
                Name = clinic.Name,
                Address = clinic.Address,
                Neighborhood = clinic.Neighborhood,
                City = clinic.City,
                State = clinic.State,
                Zipcode = clinic.Zipcode,
                CreatedAt = clinic.CreatedAt,
                UpdatedAt = clinic.UpdatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateClinicDto dto)
        {
            var clinic = await _repository.GetByIdAsync(id);

            if (clinic is null)
                return false;

            clinic.Update(
                name: dto.Name,
                address: dto.Address,
                neighborhood: dto.Neighborhood,
                city: dto.City,
                state: dto.State,
                zipcode: dto.Zipcode
            );
            clinic.UpdatedAt = DateTime.UtcNow;

            if (clinic.CreatedAt.Kind == DateTimeKind.Unspecified || clinic.CreatedAt == default)
            {
                clinic.CreatedAt = DateTime.UtcNow;
            }
            await _repository.UpdateAsync(clinic);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var clinic = await _repository.GetByIdAsync(id);

            if (clinic is null)
                return false;

            await _repository.DeleteAsync(clinic);

            return true;
        }
    }
}
