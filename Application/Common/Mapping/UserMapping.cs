using Application.Common.Abstraction;
using Application.UseCases.Users.Commands.CreateUser;
using Application.UseCases.Users.Commands.RegesterUser;
using Application.UseCases.Users.Commands.UpdateUser;
using Application.UseCases.Users.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class UserMapping : Profile
{

    ICurrentUserService _user;

    public UserMapping(ICurrentUserService user) : base()
    {
        _user = user;
    }

    public UserMapping()
    {

        _ = CreateMap<User, GetListUserResponse>()
              .ForMember(d => d.PhoneNumber, cfg => cfg.MapFrom(ent => ent.Person.PhoneNumber))
            .ForMember(d => d.FirstName, cfg => cfg.MapFrom(ent => ent.Person.FirstName))
            .ForMember(d => d.LastName, cfg => cfg.MapFrom(ent => ent.Person.LastName))
            .ForMember(d => d.Birthdate, cfg => cfg.MapFrom(ent => ent.Person.Birthdate))
            .ForMember(d => d.UserTypeName, cfg => cfg.MapFrom(ent => ent.UserType.TypeName))
            .ForMember(d => d.QuarterName, cfg => cfg.MapFrom(ent => ent.Person.Quarter.QuarterName))
            .ForMember(d => d.Language, cfg => cfg.MapFrom(ent => ent.Language.LanguageName));


        _ = CreateMap<RegisterUserCommand, User>();
        _ = CreateMap<CreateUserCommand, User>().ReverseMap();

        _ = CreateMap<UpdateUserCommand, Person>()
              .ForMember(x => x.Birthdate, x => x.MapFrom(x => DateOnly.FromDateTime(x.Birthdate)))
                     .ForMember(dest => dest.Id, opt => opt.Ignore());

        _ = CreateMap<User, UserResponse>()
                 .ForMember(d => d.PhoneNumber, cfg => cfg.MapFrom(ent => ent.Person.PhoneNumber))
            .ForMember(d => d.FirstName, cfg => cfg.MapFrom(ent => ent.Person.FirstName))
            .ForMember(d => d.LastName, cfg => cfg.MapFrom(ent => ent.Person.LastName))
            .ForMember(d => d.Birthdate, cfg => cfg.MapFrom(ent => ent.Person.Birthdate));

        _ = CreateMap<RegisterUserCommand, Person>()
            .ForMember(x => x.Birthdate, x => x.MapFrom(x => DateOnly.FromDateTime(x.Birthdate)));
    }
}
