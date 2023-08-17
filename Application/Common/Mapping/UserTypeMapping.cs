using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
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
        _ = CreateMap<CreateUserTypeCommand, UserType>()
               .ForMember(sr => sr.TypeName, des => des
                .MapFrom(y => y.userTypes
                .FirstOrDefault().TranslateText));

        _ = CreateMap<UpdateUserCommand, UserType>()
            .ForMember(x => x.Users, u => u.Ignore());


        _ = CreateMap<UserType, UserTypeResponse>()
            .ForMember(cr => cr.TypeName, cfg => cfg
              .MapFrom<UserTypeValueResolver<UserTypeResponse>>());

        _ = CreateMap<UserType, GetListUserTypeResponse>()
            .ForMember(cr => cr.TypeName, cfg => cfg
         .MapFrom<UserTypeValueResolver<GetListUserTypeResponse>>());

        _ = CreateMap<CreateCommandTranslate, TranslateUserType>();

        _ = CreateMap<UpdateCommandTranslate, TranslateUserType>();


    }
}
