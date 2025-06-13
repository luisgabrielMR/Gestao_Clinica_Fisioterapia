namespace ClinicaFisioterapiaApi.Interface.Profiles
{
    using AutoMapper;
    using ClinicaFisioterapiaApi.Domain.Entities;
    using ClinicaFisioterapiaApi.Interface.Dtos.Users;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // handled manually

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // handled manually

            CreateMap<User, UserDto>();
        }
    }
}
