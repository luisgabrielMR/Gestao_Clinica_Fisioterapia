namespace ClinicaFisioterapiaApi.Dtos.clinics
{
    public class UpdateClinicDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
    }
}
