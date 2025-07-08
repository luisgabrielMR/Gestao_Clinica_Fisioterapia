namespace ClinicaFisioterapiaApi.Interface.Dtos.People.Input;

public class CreatePersonDto
{
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
