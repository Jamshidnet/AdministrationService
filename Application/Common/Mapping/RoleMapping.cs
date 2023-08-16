using Application.Common.Mapping.ValueResolvers;
using Application.Common.Models;
using Application.UseCases.Roles.Commands;
using Application.UseCases.Roles.Responses;
using AutoMapper;
using Domein.Entities;

namespace Application.Common.Mapping;

public class RoleMapping : Profile
{
    public RoleMapping()
    {
        CreateMap<Role, RoleResponse>()
           .ForMember(cr => cr.RoleName, cfg => cfg
             .MapFrom<RoleValueResolver<RoleResponse>>());

        CreateMap<Role, GetListRoleResponse>()
            .ForMember(cr => cr.RoleName, cfg => cfg
         .MapFrom<RoleValueResolver<GetListRoleResponse>>());

        CreateMap<CreateRoleCommand, Role>()
            .ForMember(y => y.RoleName, t => t.MapFrom(q => q.roles.First().TranslateText));


        CreateMap<CreateCommandTranslate, TranslateRole>();

        CreateMap<UpdateCommandTranslate, TranslateRole>();


    }
}
