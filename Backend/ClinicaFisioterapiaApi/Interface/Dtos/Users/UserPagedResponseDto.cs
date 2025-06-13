namespace ClinicaFisioterapiaApi.Interface.Dtos.Users
{
    public class UserPagedResponseDto
    {
        public IEnumerable<UserResponseDto> Users { get; set; } = new List<UserResponseDto>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
