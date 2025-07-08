namespace ClinicaFisioterapiaApi.Interface.Dtos.People.Output;

public class PersonDto
{
    public int PersonId { get; set; }
    public string FullName { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Neighborhood { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
}
