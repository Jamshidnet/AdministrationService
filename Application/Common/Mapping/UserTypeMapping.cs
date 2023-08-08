using Application.UseCases.Users.Commands.UpdateUser;
using Application.UseCases.UserTypes.Commands;
using Application.UseCases.UserTypes.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class UserTypeMapping : Profile
{
    public UserTypeMapping()
    {
        CreateMap<CreateUserTypeCommand, UserType>();
        CreateMap<UpdateUserCommand, UserType>()
            .ForMember(x => x.Users, u => u.Ignore());
        CreateMap<UserType, UserTypeResponse>();
        CreateMap<UserType, GetListUserTypeResponse>();
    }
}
