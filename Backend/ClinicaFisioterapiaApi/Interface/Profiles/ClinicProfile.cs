namespace ClinicaFisioterapiaApi.Interface.Profiles
{
    using AutoMapper;
    using ClinicaFisioterapiaApi.Domain.Entities;
    using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Input;
    using ClinicaFisioterapiaApi.Interface.Dtos.Clinics.Output;

    public class ClinicProfile : Profile
    {
        public ClinicProfile()
        {
            CreateMap<CreateClinicDto, Clinic>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); 

            CreateMap<Clinic, ClinicDto>();
        }
    }
}
