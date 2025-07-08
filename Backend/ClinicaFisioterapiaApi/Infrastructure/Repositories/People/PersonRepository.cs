using ClinicaFisioterapiaApi.Domain.Entities;
using ClinicaFisioterapiaApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaFisioterapiaApi.Infrastructure.Repositories.People;

public class PersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.People.AsNoTracking().ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        return await _context.People.FindAsync(id);
    }

    public async Task AddAsync(Person person)
    {
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        person.CreatedAt = DateTime.SpecifyKind(person.CreatedAt, DateTimeKind.Utc);
        person.UpdatedAt = DateTime.UtcNow;

        _context.People.Update(person);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        person.DeletedAt = DateTime.UtcNow;
        _context.People.Update(person);
        await _context.SaveChangesAsync();
    }
}
