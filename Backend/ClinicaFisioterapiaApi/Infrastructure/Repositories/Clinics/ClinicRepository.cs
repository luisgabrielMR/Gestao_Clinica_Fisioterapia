using ClinicaFisioterapiaApi.Common.Helpers;
using ClinicaFisioterapiaApi.Domain.Entities;
using ClinicaFisioterapiaApi.Application.Interfaces.Repositories;
using ClinicaFisioterapiaApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaFisioterapiaApi.Infrastructure.Repositories.Clinics;

public class ClinicRepository : IClinicRepository
{
    private readonly AppDbContext _context;

    public ClinicRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Clinic clinic)
    {
        _context.Clinics.Add(clinic);
        await _context.SaveChangesAsync();
    }

    public async Task<Clinic?> GetByIdAsync(int id)
    {
        return await _context.Clinics
            .FirstOrDefaultAsync(c => c.ClinicId == id);
    }

    public async Task<IEnumerable<Clinic>> GetAllNotDeletedAsync()
    {
        return await _context.Clinics
            .ToListAsync();
    }

    public async Task<IEnumerable<Clinic>> GetPagedAsync(string? filter, string? sort, int page, int size)
    {
        var query = _context.Clinics.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(c => c.Name.ToLower().Contains(filter.ToLower()));
        }

        query = QueryHelper.ApplySorting(query, sort, "ClinicId");

        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task UpdateAsync(Clinic clinic)
    {
        clinic.UpdatedAt = DateTime.UtcNow;
        if (clinic.CreatedAt.Kind == DateTimeKind.Unspecified)
            clinic.CreatedAt = DateTime.SpecifyKind(clinic.CreatedAt, DateTimeKind.Utc);

        _context.Clinics.Update(clinic);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        var clinic = await _context.Clinics.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.ClinicId == id);
        if (clinic != null)
        {
            clinic.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
