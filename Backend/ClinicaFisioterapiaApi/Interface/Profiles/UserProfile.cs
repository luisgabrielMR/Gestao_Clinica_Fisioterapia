namespace ClinicaFisioterapiaApi.Interface.Profiles
{
    using AutoMapper;
    using ClinicaFisioterapiaApi.Domain.Entities;
    using ClinicaFisioterapiaApi.Interface.Dtos.Users.Input;
    using ClinicaFisioterapiaApi.Interface.Dtos.Users.Output;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); 

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); 

            CreateMap<User, UserDto>();
        }
    }
}
