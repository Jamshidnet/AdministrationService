using Application.UseCases.Users.Commands.CreateUser;
using Application.UseCases.Users.Commands.RegesterUser;
using Application.UseCases.Users.Commands.UpdateUser;
using Application.UseCases.Users.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, GetListUserResponse>()
              .ForMember(d => d.PhoneNumber, cfg => cfg.MapFrom(ent => ent.Person.PhoneNumber))
            .ForMember(d => d.FirstName, cfg => cfg.MapFrom(ent => ent.Person.FirstName))
            .ForMember(d => d.LastName, cfg => cfg.MapFrom(ent => ent.Person.LastName))
            .ForMember(d => d.Birthdate, cfg => cfg.MapFrom(ent => ent.Person.Birthdate))
            .ForMember(d => d.UserTypeName , cfg => cfg.MapFrom(ent => ent.UserType.TypeName))
            .ForMember(d => d.QuarterName , cfg => cfg.MapFrom(ent => ent.Person.Quarter.QuarterName));
        CreateMap<RegisterUserCommand, User>().ReverseMap();
        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<UpdateUserCommand, Person>()
              .ForMember(x => x.Birthdate, x => x.MapFrom(x => DateOnly.FromDateTime(x.Birthdate)))
                     .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<User,UserResponse>()
                 .ForMember(d => d.PhoneNumber, cfg => cfg.MapFrom(ent => ent.Person.PhoneNumber))
            .ForMember(d => d.FirstName, cfg => cfg.MapFrom(ent => ent.Person.FirstName))
            .ForMember(d => d.LastName, cfg => cfg.MapFrom(ent => ent.Person.LastName))
            .ForMember(d => d.Birthdate, cfg => cfg.MapFrom(ent => ent.Person.Birthdate));
        CreateMap<RegisterUserCommand, Person>()
            .ForMember(x => x.Birthdate, x => x.MapFrom(x => DateOnly.FromDateTime(x.Birthdate)));
    }
}
