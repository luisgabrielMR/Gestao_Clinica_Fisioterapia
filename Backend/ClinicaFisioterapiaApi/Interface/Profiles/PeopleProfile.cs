using AutoMapper;
using ClinicaFisioterapiaApi.Domain.Entities;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Input;
using ClinicaFisioterapiaApi.Interface.Dtos.People.Output;

namespace ClinicaFisioterapiaApi.Interface.Profiles;

public class PeopleProfile : Profile
{
    public PeopleProfile()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<CreatePersonDto, Person>();
    }
}
