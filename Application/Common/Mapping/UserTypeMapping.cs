using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.Categories.Responses;
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
        CreateMap<CreateUserTypeCommand, UserType>()
               .ForMember(sr => sr.TypeName, des => des
                .MapFrom(y => y.userTypes
                .FirstOrDefault().TranslateText));

        CreateMap<UpdateUserCommand, UserType>()
            .ForMember(x => x.Users, u => u.Ignore());


        CreateMap<UserType, UserTypeResponse>()
            .ForMember(cr => cr.TypeName, cfg => cfg
              .MapFrom<UserTypeValueResolver<UserTypeResponse>>());

        CreateMap<UserType, GetListUserTypeResponse>()
            .ForMember(cr => cr.TypeName, cfg => cfg
         .MapFrom<UserTypeValueResolver<GetListUserTypeResponse>>());

        CreateMap<CreateCommandTranslate, TranslateUserType>();

        CreateMap<UpdateCommandTranslate, TranslateUserType>();


    }
}
