namespace ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Output;

public class ClinicDto
{
    public int ClinicId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? Neighborhood { get; set; }
    public string City { get; set; } = string.Empty;
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
