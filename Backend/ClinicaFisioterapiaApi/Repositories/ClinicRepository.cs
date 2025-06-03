using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClinicaFisioterapiaApi.Data;
using ClinicaFisioterapiaApi.Interfaces.Clinic;
using ClinicaFisioterapiaApi.Models;

namespace ClinicaFisioterapiaApi.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _context.Clinics.ToListAsync();
        }

        public async Task<Clinic?> GetByIdAsync(int id)
        {
            return await _context.Clinics.FindAsync(id);
        }

        public async Task AddAsync(Clinic clinic)
        {
            await _context.Clinics.AddAsync(clinic);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Clinic clinic)
        {
            _context.Clinics.Update(clinic);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Clinic clinic)
        {
            _context.Clinics.Remove(clinic);
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
