namespace ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Input;

public class CreateClinicDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Neighborhood { get; set; }
    public string City { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? ZipCode { get; set; }
}
