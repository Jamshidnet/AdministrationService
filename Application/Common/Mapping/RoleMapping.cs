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
        CreateMap<Role, RoleResponse>();
        CreateMap<Role, GetListRoleResponse>();

        CreateMap<CreateRoleCommand, Role>()
            .ForMember(y => y.RoleName, t => t.MapFrom(q => q.roles.First().TranslateText));


        CreateMap<CreateCommandTranslate, TranslateRole>();

        CreateMap<UpdateCommandTranslate, TranslateRole>();


    }
}
