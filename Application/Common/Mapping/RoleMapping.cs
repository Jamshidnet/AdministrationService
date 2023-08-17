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
        _ = CreateMap<Role, RoleResponse>()
           .ForMember(cr => cr.RoleName, cfg => cfg
             .MapFrom<RoleValueResolver<RoleResponse>>());

        _ = CreateMap<Role, GetListRoleResponse>()
            .ForMember(cr => cr.RoleName, cfg => cfg
         .MapFrom<RoleValueResolver<GetListRoleResponse>>());

        _ = CreateMap<CreateRoleCommand, Role>()
            .ForMember(y => y.RoleName, t => t.MapFrom(q => q.roles.First().TranslateText));


        _ = CreateMap<CreateCommandTranslate, TranslateRole>();

        _ = CreateMap<UpdateCommandTranslate, TranslateRole>();


    }
}
